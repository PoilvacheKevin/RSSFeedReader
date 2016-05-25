using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Xml.Linq;

namespace Lecteur_RSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private List<RSS> laListe;
        private Files mySave = new Files("remember.csv");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rssWc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //1 - parsez le resultat du telechargement grace a l'objet Xelement
            XElement flux = XElement.Parse(e.Result);
        
            //2 - on va maintenant recuperer l'ensemble des articles presents sous le noeud "item"
            var requete = from item in flux.Descendants("item")
            select new RSS()
            {
                Description = item.Element("description").Value,
                Title = item.Element("title").Value,
                Link = item.Element("link").Value
            };
       
            //3 - on va mettre l'ensemble des items dans une liste
            laListe = requete.ToList();

            // Finalement on rempli notre listBox avec le contenu de notre liste

            /* Les habitués du .Net aurrait surrement preféré que fasse un Binding,

            * mais c'est deliberement que je n'en parle pas, ca fera l'objet d'un autre article

            */
            foreach (RSS article in laListe)
            {
                LB_RSSFlow.Items.Add(article.Title);
            }
        }

        private void BT_Valider_Click(object sender, RoutedEventArgs e)
        {
            mySave.AddLink(TB_Search.Text);
            // création d'un client web
            WebClient rssWc = new WebClient();
            // on s'abonne à la fin du telechargement
            rssWc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(rssWc_DownloadStringCompleted);
            // on va definir l'encodage en utf-8
            rssWc.Encoding = Encoding.UTF8;
            // telechargement de notre flux rss
            rssWc.DownloadStringAsync(new Uri(TB_Search.Text, UriKind.Absolute));
        }

        private void LB_RSSFlow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 1- on recupere la position sur laquelle on a cliqué
            int positionDeLarticle = LB_RSSFlow.SelectedIndex;
            // 2 - on utilise maintenant cette postion pour rechercher dans la liste
            TB_NewTitle.Text = laListe.ElementAt(positionDeLarticle -1).Title;
            TB_newContents.Text = laListe.ElementAt(positionDeLarticle - 1).Description;
        }
    }
}
