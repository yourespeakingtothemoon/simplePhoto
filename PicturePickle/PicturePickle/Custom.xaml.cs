using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;

namespace PicturePickle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Custom : ContentPage
    {

        private Color color = new Color(255, 255, 255);
        //LinkedList<Filter> filters;
        int kernelSize = 7;
        int bloomThreshold = 200;
        string currentFilterString = "";
        //LinkedList<Filter> filtersPtr;

        public Custom()
        {
            InitializeComponent();

        }

        private void filterAddition(object sender, EventArgs e)
        {
            
            Color colorVal;
            Filter filter=null;
        
            Button btn = sender as Button;
            int red1, green1, blue1, red2, green2, blue2 = 255;
            int.TryParse(usrR.Text, out red1);
            int.TryParse(usrG.Text, out green1);
            int.TryParse(usrB.Text, out blue1);
            int.TryParse(usrR2.Text, out red2);
            int.TryParse(usrG2.Text, out green2);
            int.TryParse(usrB2.Text, out blue2);
            Color start = new Color(red1, green1, blue1);
            Color end = new Color(red2, green2, blue2);
            switch (btn.Text)
            {
                case "Add":   
                case "Subtract":
                case "Multiply":   
                case "Divide":

                    //colorVal.Set(new Color(int.PusrR.Text))
                    if (btn.Text == "Add") { filter = new Add(start); }
                    if (btn.Text == "Subtract")
                    { start.ToAdditiveInverse(); filter = new Add(start); }
                    if (btn.Text == "Multiply")
                    { filter = new Multiply(start); }
                    if (btn.Text == "Divide")
                    { start.ToMultiplicitiveInverse(); filter = new Multiply(start); }
                    break;
                    //layer 2
                case "Blur":
                    int blurAmt = 1;
                    int blurTimes = 10;
                    int.TryParse(usrBA.Text, out blurAmt);
                    int.TryParse(usrBT.Text, out blurTimes);
                    filter = new Blur(blurAmt, blurTimes);
                    break;
                case "Kuwahara":
                    int kernel = 7;
                    int.TryParse(usrK.Text, out kernel);
                    filter = new Kuwahara(kernel);
                    break;
                case "Bloom":
                    int threshold = 200;
                    int.TryParse(usrT.Text, out threshold);
                    filter = new Bloom(threshold);
                    break;
               //layer 3
                case "Adjust Value":
                    float val = -.5f;
                    float.TryParse(usrV.Text, out val);
                    filter = new ValueAdj(val);
                    break;
                    
                case "Adjust Lightness":
                    float val2 = .5f;
                    float.TryParse(usrL.Text, out val2);
                    filter = new Lightness(val2);
                    break;
                   
                case "Make Grayscale":
                    
                    filter = new Grayscale();
                    break;
                case "Invert Color":

                    filter = new Invert();
                    break;
                case "Gradient":
                
                   
                   
                
                    switch (directionSel.SelectedIndex)
                    {
                        case 0:
                            //left to right
                            filter = new LinearGradient(start,end);
                            break;
                        case 1:
                            //right to left
                            filter = new LinearGradient(end,start);
                            break;
                        case 2:
                            //bottom to top
                            filter = new LinearGradient(end,start,true);
                            break;
                        case 3:
                            //top to bottom
                            filter = new LinearGradient(start,end,true);
                            break;
                    
                      
                    }
                    break;
                case "Interpolate":
                    filter = new Interpolate(start, end);
                    break;
                case "Two Tone":
                    int cut;
                    int.TryParse(usrCut.Text, out cut);
                    filter = new TwoTone(start, end, cut);
                    break;
                case "Stripes":
                    filter = new Stripes();
                    break;
            }

            if (filter != null)
            {
                App.main.filters.AddLast(filter);
             
                currentFilterString += filter.toString()+" | ";
            }

            filterLbl.Text = currentFilterString;

        }

        private void usrApply_Clicked(object sender, EventArgs e)
        {
           
            Navigation.PopModalAsync();
            App.main.applyFilters_Clicked(sender, e);
        }

        private void usrClear_Clicked(object sender, EventArgs e)
        {
            App.main.filters.Clear();
            currentFilterString = "";
            filterLbl.Text = currentFilterString;
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            currentFilterString = "";
            Navigation.PopModalAsync();
        }
    }
}