using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stagev2
{
    class CreneauPlanning
    {
        public DateTime HeureDebut { get; set; }
        public DateTime HeureFin { get; set; }
        public string Type { get; set; }        // "Préparation" ou "Épreuve"
        public string Discipline { get; set; }  // Mathématiques, HGGSP…
        public string Libelle { get; set; }      // Affiché dans la grille
      
    }
}
