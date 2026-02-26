using System.Data;
using System.Windows.Forms;

namespace stagev2
{
    public partial class Form1 : Form
    {
        private DataTable _tableCandidats;

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Bouton Quitter l'application
            var resultat = MessageBox.Show(
           "Voulez-vous vraiment quitter l'application ?",
           "Confirmation",
           MessageBoxButtons.YesNo, //oui ou non , si l'utilisateur veut quitter + demander
           MessageBoxIcon.Question);

            if (resultat == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Bouton Agrandir la fenetre
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Bouton Retrecir la fenetre
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
        private void btnParcourir_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                // Chercher dans dossiers les fichiers CSV + le selectionner
                Filter = "Fichiers CSV (*.csv)|*.csv",
                Title = "Sélectionner un fichier CSV"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {


                DataTable table = LireCsvAvecPrioriteGlobale(openFileDialog.FileName);
                dataGridView1.DataSource = table;



                if (dataGridView1.Columns.Contains("Priorite"))
                    dataGridView1.Columns["Priorite"].Visible = false;

            }
        }
        private bool EstHandiOui(string[] valeurs, string[] entetes)
        {
            int index = Array.IndexOf(entetes, "HANDI");
            if (index == -1) return false;

            return valeurs[index]
                .Trim()
                .Equals("Oui", StringComparison.OrdinalIgnoreCase);
        }

        private void CompterValeur(string[] valeurs, string[] entetes, string colonne, Dictionary<string, int> compteur)
        {
            int index = Array.IndexOf(entetes, colonne);
            if (index == -1) return;

            string valeur = valeurs[index]?.Trim();
            if (string.IsNullOrEmpty(valeur)) return;

            if (!compteur.ContainsKey(valeur))
                compteur[valeur] = 0;

            compteur[valeur]++;
        }

        private bool EstValeurOrpheline(string[] valeurs, string[] entetes, string colonne, Dictionary<string, int> compteur)
        {
            int index = Array.IndexOf(entetes, colonne);
            if (index == -1) return false;

            string valeur = valeurs[index]?.Trim();
            if (string.IsNullOrEmpty(valeur)) return false;

            return compteur.ContainsKey(valeur) && compteur[valeur] == 1;
        }


        private DataTable LireCsvAvecPrioriteGlobale(string cheminFichier)
        {
            DataTable dt = new DataTable();
            string[] lignes = File.ReadAllLines(cheminFichier);

            if (lignes.Length == 0)
                return dt;

            string[] entetes = lignes[0].Split(';');

            foreach (string entete in entetes)
                dt.Columns.Add(entete);

            dt.Columns.Add("Priorite", typeof(int));

            // Comptage des choix disciplinaires de toutes les matieres
            Dictionary<string, int> compteurDisc1 = new Dictionary<string, int>();
            Dictionary<string, int> compteurDisc2 = new Dictionary<string, int>();

            for (int i = 1; i < lignes.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lignes[i])) continue;

                string[] valeurs = lignes[i].Split(';');

                CompterValeur(valeurs, entetes, "CHOIX DISCIPLINE 1", compteurDisc1);
                CompterValeur(valeurs, entetes, "CHOIX DISCIPLINE 2", compteurDisc2);
            }

            // Calcul des priorités
            for (int i = 1; i < lignes.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lignes[i])) continue;

                string[] valeurs = lignes[i].Split(';');
                DataRow row = dt.NewRow();

                for (int j = 0; j < entetes.Length; j++)
                    row[j] = valeurs[j];

                int priorite = 4; // défaut

                // 1️⃣ PRIORITÉ MAJEURE : HANDI
                if (EstHandiOui(valeurs, entetes))
                {
                    priorite = 1;
                }
                // 2️⃣ Discipline 1 orpheline
                else if (EstValeurOrpheline(valeurs, entetes, "CHOIX DISCIPLINE 1", compteurDisc1))
                {
                    priorite = 2;
                }
                // 3️⃣ Discipline 2 orpheline
                else if (EstValeurOrpheline(valeurs, entetes, "CHOIX DISCIPLINE 2", compteurDisc2))
                {
                    priorite = 3;
                }

                row["Priorite"] = priorite;
                dt.Rows.Add(row);
            }

            dt.DefaultView.Sort = "Priorite ASC";
            return dt.DefaultView.ToTable();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView2.ReadOnly = true;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.AllowUserToAddRows = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];

                string discipline1 = row.Cells["CHOIX DISCIPLINE 1"].Value?.ToString() ?? "";
                string discipline2 = row.Cells["CHOIX DISCIPLINE 2"].Value?.ToString() ?? "";

                GenererEmploiDuTemps(discipline1, discipline2);
            }
        }
        private void GenererEmploiDuTemps(string discipline1, string discipline2)
        {
            // Vider la DataGridView2
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();

            // Ajouter les colonnes
            dataGridView2.Columns.Add("Jour", "Jour");
            dataGridView2.Columns.Add("Heure", "Heure");
            dataGridView2.Columns.Add("Discipline1", "Discipline 1");
            dataGridView2.Columns.Add("Discipline2", "Discipline 2");

            // Générer l’emploi du temps
            // Exemple simple : on crée un tableau fixe avec des heures
            string[] heures = { "08:00", "09:00", "10:00", "11:00", "12:00" };
            string[] jours = { "Lundi", "Mardi", "Mercredi" };
            for (int i = 0; i < heures.Length; i++)
            {
                dataGridView2.Rows.Add(
                    jours[i],
                    heures[i],
                    discipline1,
                    discipline2);

            }
        }
                  
        

        private void Form1_Load(object sender, EventArgs e)
        {
            // CONFIGURATION DataGridView 1 (Liste candidats)
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;

            // CONFIGURATION DataGridView 2 (Emploi du temps)
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;


            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            ChargerCsv(); // ⬅️ OBLIGATOIRE AVANT TOUTE SÉLECTION

        }
        private void ChargerCsv()
        {
            string cheminCsv = @"C:\Users\cindy\OneDrive\Documents\csv\listeDisciplinesCandidatschoix SPE.csv";

            if (!File.Exists(cheminCsv))
            {
                MessageBox.Show("Fichier CSV introuvable.");
                return;
            }

            _tableCandidats = LireCsvAvecPrioriteGlobale(cheminCsv);

            if (_tableCandidats == null || _tableCandidats.Rows.Count == 0)
            {
                MessageBox.Show("CSV vide ou non chargé");
                return;
            }

            // Création colonne CANDIDAT (une seule fois)
            if (!_tableCandidats.Columns.Contains("CANDIDAT"))
            {
                _tableCandidats.Columns.Add("CANDIDAT", typeof(string));

                foreach (DataRow row in _tableCandidats.Rows)
                {
                    row["CANDIDAT"] =
                        $"{row["NOM"]} {row["PRENOM"]} ({row["IDCANDIDAT"]})";
                }
            }

            dataGridView1.DataSource = null;        // sécurité
            dataGridView1.DataSource = _tableCandidats;

            // Mode liste
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.Visible = false;

            dataGridView1.Columns["CANDIDAT"].Visible = true;
            dataGridView1.Columns["CANDIDAT"].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}



