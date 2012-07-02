using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace PanoramaIMCCalculator
{
    public partial class ZoneDetailPage : PhoneApplicationPage
    {
        public ZoneDetailPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool TryGetQueryString(string key, out string value)
        {
            string valueString;
            if (NavigationContext.QueryString.TryGetValue(key, out valueString))
            {
                value = valueString;
                return true;
            }
            value = null;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string type = null;
            if (TryGetQueryString("type", out type))
            {
                Console.WriteLine(type);
            }
            string gender = null;
            if (TryGetQueryString("gender", out gender))
            {
                Console.WriteLine(gender);
            }
            if (gender == "Homme")
                gender = "";
            ZoneImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/BackTile" + type + gender + ".png", UriKind.RelativeOrAbsolute));
            
            
            if (type == "Denutrition")
            {
                setDenutritionDataContext();
            }
            if (type == "Maigreur")
            {
                setMaigreurDataContext();
            }
            if (type == "Normal")
            {
                setNormalDataContext();
            }
            if (type == "Surpoids")
            {
                setSurpoidsDataContext();
            }
            if (type == "Obese")
            {
                setObesiteDataContext();
            }
            if (type == "ObesiteSevere")
            {
                setObesiteDataContext();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void setDenutritionDataContext()
        {
            PageTitle.Text = AppResources.DenutritionDetailPageTitle;

            zoneDetailTextBlock.Text = AppResources.DenutritionDetailPageContent;
                /*"Dénutrition : \n" +
                "La dénutrition est un état pathologique résultant d'apports nutritionnels insuffisants en regard des dépenses énergétiques de l'organisme. Lorsque les apports sont inadaptés en plus d'être insuffisants, on parle de malnutrition.\n" +
                "La dénutrition est classée dans la section marasme nutritionnel de la classification internationale des maladies.\n" +
                "\n" +
                "Physiopathologie\n" +
                "La dénutrition peut avoir de multiple origines. L'étiologie en est variée et peut associer une affection organique, psychiatrique ou sociale. La dénutrition est dite primaire lorsque celle-ci est induite par une cause directe, et secondaire lorsqu'elle est provoquée des suite d'une autre affection. On distingue deux mécanismes physiopathologiques : la carence d'apports et l'hypermétabolisme.\n" +
                "\n" +
                "Dénutrition par carence d'apports\n" +
                "En cas de carence d'apports se met en place l'état de jeûne, qui évolue selon plusieurs étapes. Les sources d'énergie dans le jeûne sont, pour le métabolisme glucidique, l'utilisation du glycogène hépatique ainsi que la synthèse de glucose et, pour le métabolisme lipidique, l'utilisation des acides gras et la cétogenèse.\n" +
                "\n" +
                "* Jeûne immédiat : adaptation à la prise discontinue de nourriture depuis moins de douze heures. La sécrétion d'insuline diminue, tandis que celle de glucagon augmente. Ce jeu hormonal entraîne une stimulation de la lipolyse et de l'oxydation des acides gras, puis une cétogenèse. Afin de maintenir la glycémie, la glycogénolyse est stimulée, de manière exclusive.\n" +
                "\n" +
                "* Jeûne court : adaptation à l'absence de prise alimentaire sur une durée de douze heures à trois ou quatre jours. La sécrétion d'insuline diminue encore. L'épuisement des réserves de glycogène entraîne une baisse de la glycémie. La seule source de glucose de l'organisme devient la néoglucogenèse, qui fabrique du glucose à partir des acides aminés des protéines musculaires en produisant de l'urée comme déchet. L'excrétion de l'urée augmente donc. La cétogenèse se poursuit.\n" +
                "\n" +
                "* Jeûne prolongé : après cinq jours environ jusqu'à plusieurs semaines. Les corps cétoniques plasmatiques augmentent, tandis que l'excrétion d'urée de stabilise à 50 mg/kg/j. Cette stabilisation est en rapport avec une stabilisation de la protéolyse à visée d'épargne protéique. De nouvelles modifications hormonales se produisent, avec une diminution de la production des hormones thyroïdiennes. Cette phase de jeûne prolongé aboutit à l'état de marasme.\n" +
                "\n" +
                "* Phase terminale : Lorsque les réserves lipidiques sont épuisées, les taux plasmatiques d'acides gras et de corps cétoniques s'abaissent, tandis que la glycémie remonte. En effet, on observe alors un surcroît de mobilisation des protéines des muscles squelettiques pour la néoglucogenèse. Cette dernière entraîne un accroissement de l'excrétion d'urée et d'azote et se solde par une forte morbi-mortalité.\n" +
                "\n" +
                "Credits Wikipedia : http://fr.wikipedia.org/wiki/Dénutrition";*/
        }

        /// <summary>
        /// 
        /// </summary>
        private void setMaigreurDataContext()
        {
            PageTitle.Text = AppResources.MaigreurDetailPageTitle;

            zoneDetailTextBlock.Text = AppResources.MaigreurDetailPageContent;
                /*"Maigreur :\n" +
                "\n" +
                "La maigreur est l'état de ce qui est maigre. Dans le cas d'une personne, on parle d'insuffisance pondérale. À ce titre, elle est définie par l'Organisation mondiale de la santé comme la condition anormale des individus dont l'indice de masse corporelle est comprise entre 15 et 18,5.\n" +
                "La maigreur pathologique augmente le risque de maladies diverses parmi lesquelles on peut citer les infections ou l'ostéoporose mais peut également être le symptôme de l'une d'elles. Elle peut être également un état non pathologique, c'est-à-dire constitutionnelle, stable et ne s'accompagnant d'aucun trouble.\n" +
                "ÉtiologieLes causes peuvent être\n" +
                "\n" +
                "* sociologiques : famine, pauvreté\n" +
                "* psychiatriques : anorexie mentale, dépression\n" +
                "* médicales : néoplasie, infection grave (tuberculose...), diabète de type 1, toxicomanie, maladie intestinale avec malabsorption, SIDA...\n" +
                "* Génétiques : héréditaire\n" +
                "* Morphologiques : os fins, muscles fins\n" +
                "* La maigreur constitutionnelle\n" +
                "\n" +
                "Credits Wikipedia : http://fr.wikipedia.org/wiki/Surpoids";*/
        }

        /// <summary>
        /// 
        /// </summary>
        private void setNormalDataContext()
        {
            PageTitle.Text = AppResources.NormalDetailPageTitle;

            zoneDetailTextBlock.Text = AppResources.NormalDetailPageContent;
                /*"Il ne faut pas confondre la masse (grave) et le poids.\n" +
                "Le poids d'un corps est une force, due principalement à l'action qu'exerce sur lui le champ gravitationnel.\n" +
                "\n" +
                "Réserves à l'égard de l'IMC\n" +
                "Il est important de garder à l’esprit que l’IMC n’est qu’un indicateur, non pas une donnée absolue. Du fait de leur masse musculaire, certains sportifs ont un indice de masse corporelle supérieur à 25 kg/m2, sans qu’ils encourent de danger. De plus, selon la morphologie d’une personne, son IMC de bonne forme varie. Une personne peut être trapue sans être grasse (par exemple Bixente Lizarazu, Mike Tyson ou Jonah Lomu), et une autre peut être longiligne mais avoir une masse graisseuse trop importante.\n" +
                "Par ailleurs, il faut garder à l'esprit les limites des seuils recommandés par l'OMS. S'ils sont pratiques à utiliser, ces seuils devraient idéalement varier selon le sexe, l'âge et l'origine ethnique et ces derniers ne doivent s'appliquer qu'avec prudence au diagnostic individuel.\n" +
                "Le jugement de son poids au moyen de l'indice de masse grasse doit donc se faire avec l'aide d'un médecin et la consultation d’un médecin nutritionniste ou d’un diététicien diplômé est recommandée.\n" +
                "\n" +
                "Credits Wikipedia : http://fr.wikipedia.org/wiki/Indice_de_masse_corporelle";*/
        }

        /// <summary>
        /// 
        /// </summary>
        private void setSurpoidsDataContext()
        {
            PageTitle.Text = AppResources.SurpoidsDetailPageTitle;

            zoneDetailTextBlock.Text = AppResources.SurpoidsDetailPageContent;
                /*"Le surpoids est l'état d'une personne présentant une corpulence considérée comme légèrement plus importante que la normale ou la moyenne dans une société donnée.\n" +
                "\n" +
                "Il est définie par l'Organisation mondiale de la santé comme l'attribut des individus présentant un indice de masse corporelle compris entre 25 et 30 kilogrammes par mètres carrés et est, selon ces critères, bornée par la minceur (ou corpulence normale) et l'obésité modérée.\n" +
                "Credits Wikipedia : http://fr.wikipedia.org/wiki/Surpoids";*/
        }

        /// <summary>
        /// 
        /// </summary>
        private void setObesiteDataContext()
        {
            PageTitle.Text = AppResources.ObesiteDetailPageTitle;

            zoneDetailTextBlock.Text = AppResources.ObesiteDetailPageContent;
                /*"L'obésité est l'état d'un individu ayant une masse corporelle largement supérieure à ce qui est souhaitable ou acceptable, généralement dû à une accumulation de masse adipeuse. L'obésité humaine a été reconnue comme une maladie en 1997 par l'OMS. Cette organisation définit « le surpoids et l'obésité comme une accumulation anormale ou excessive de graisse corporelle qui peut nuire à la santé ». Sa prévention est un problème de santé publique dans les pays développés. Elle peut avoir des répercussions importantes sur la santé de l'individu.\n" +
                "Cette maladie multifactorielle est considérée aujourd'hui par métaphore comme une pandémie, bien qu'il ne s'agisse pas d'une maladie infectieuse.\n" +
                "\n" +
                "Définition\n" +
                "Les formes cliniques sont nombreuses, avec des mécanismes physiopathologiques et des conséquences pathologiques différents, il est donc plus judicieux de parler « des obésités ». Pour évaluer ces obésités il convient d'analyser deux paramètres qui influent sur les complications de la maladie d'une manière indépendante l'un de l'autre : l'excès de masse grasse, et la répartition du tissu adipeux.\n" +
                "\n" +
                "Phénotypes\n" +
                "4 types d'obésité sont décrits :\n" +
                "* le type I : le surplus de graisse est réparti au niveau du corps sans localisation préférentielle ;\n" +
                "* le type II : l'excès de graisse est concentré au niveau du tronc et de l'abdomen : il est question d'obésité androïde ;\n" +
                "* le type III : l'accumulation de graisse se fait dans l'abdomen : il est question d'obésité viscérale ;\n" +
                "* le type IV : la graisse se localise au niveau des hanches et des cuisses (niveau glutéofémoral) : c'est une obésité gynoïde.\n" +
                "\n" +
                "Credits Wikipedia : http://fr.wikipedia.org/wiki/Obésité";*/
        }

        /// <summary>
        /// 
        /// </summary>
        private void setObesiteSevereDataContext()
        {
            PageTitle.Text = AppResources.ObesiteSevereDetailPageTitle;

            zoneDetailTextBlock.Text = AppResources.ObesiteSevereDetailPageContent;
                /*"L'obésité est l'état d'un individu ayant une masse corporelle largement supérieure à ce qui est souhaitable ou acceptable, généralement dû à une accumulation de masse adipeuse. L'obésité humaine a été reconnue comme une maladie en 1997 par l'OMS. Cette organisation définit « le surpoids et l'obésité comme une accumulation anormale ou excessive de graisse corporelle qui peut nuire à la santé ». Sa prévention est un problème de santé publique dans les pays développés. Elle peut avoir des répercussions importantes sur la santé de l'individu.\n" +
                "Cette maladie multifactorielle est considérée aujourd'hui par métaphore comme une pandémie, bien qu'il ne s'agisse pas d'une maladie infectieuse.\n" +
                "\n" +
                "Définition\n" +
                "Les formes cliniques sont nombreuses, avec des mécanismes physiopathologiques et des conséquences pathologiques différents, il est donc plus judicieux de parler « des obésités ». Pour évaluer ces obésités il convient d'analyser deux paramètres qui influent sur les complications de la maladie d'une manière indépendante l'un de l'autre : l'excès de masse grasse, et la répartition du tissu adipeux.\n" +
                "\n" +
                "Phénotypes\n" +
                "4 types d'obésité sont décrits :\n" +
                "* le type I : le surplus de graisse est réparti au niveau du corps sans localisation préférentielle ;\n" +
                "* le type II : l'excès de graisse est concentré au niveau du tronc et de l'abdomen : il est question d'obésité androïde ;\n" +
                "* le type III : l'accumulation de graisse se fait dans l'abdomen : il est question d'obésité viscérale ;\n" +
                "* le type IV : la graisse se localise au niveau des hanches et des cuisses (niveau glutéofémoral) : c'est une obésité gynoïde.\n" +
                "\n" +
                "Credits Wikipedia : http://fr.wikipedia.org/wiki/Obésité";*/
        }
    }
}