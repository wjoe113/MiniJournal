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
using Microsoft.Win32;
using Microsoft.Ink;
using System.IO;
using System.Windows.Ink;

namespace MiniJournal3._0
{
    ///MiniJournal3.0 by Joe Wileman
    ///09-16-16 CAP6105:Pen-based User Interfaces
    public partial class MainWindow : Window
    {
        private Button currentButton;
        private bool usingGraphPaper;
        int trigSize = 0;

        //Build the main window
        public MainWindow()
        {
            InitializeComponent();
            currentButton = button1;
            usingGraphPaper = false;
        }

        //Black pen
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.InkAndGesture; //Sets the editing mode to Pen
            currentButton = button1;
            button10.Background = currentButton.Background; //Chnages the display color to the current button
            button10.Content = ""; //No text in this button
            var drawingAttributes = myInkCanvas.DefaultDrawingAttributes;
            drawingAttributes.Color = Colors.Black; //Pen color:black
            drawingAttributes.IsHighlighter = false; //Pens are not highlighters
        }

        //Red pen
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.InkAndGesture;
            currentButton = button2;
            button10.Background = currentButton.Background;
            button10.Content = "";
            var drawingAttributes = myInkCanvas.DefaultDrawingAttributes;
            drawingAttributes.Color = Colors.Red;
            drawingAttributes.IsHighlighter = false;
        }

        //Yellow pen
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.InkAndGesture;
            currentButton = button3;
            button10.Background = currentButton.Background;
            button10.Content = "";
            var drawingAttributes = myInkCanvas.DefaultDrawingAttributes;
            drawingAttributes.Color = Colors.Yellow;
            drawingAttributes.IsHighlighter = false;
        }

        //Green pen
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.InkAndGesture;
            currentButton = button4;
            button10.Background = currentButton.Background;
            button10.Content = "";
            var drawingAttributes = myInkCanvas.DefaultDrawingAttributes;
            drawingAttributes.Color = Colors.Green;
            drawingAttributes.IsHighlighter = false;
        }

        //Blue pen
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.InkAndGesture;
            currentButton = button5;
            button10.Background = currentButton.Background;
            button10.Content = "";
            var drawingAttributes = myInkCanvas.DefaultDrawingAttributes;
            drawingAttributes.Color = Colors.Blue;
            drawingAttributes.IsHighlighter = false;
        }

        //Yellow highlighter
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.Ink; //Highlighter not set to gesture, only pens
            currentButton = button6;
            button10.Background = currentButton.Background;
            button10.Content = "";
            var drawingAttributes = myInkCanvas.DefaultDrawingAttributes;
            drawingAttributes.Color = (Color)ColorConverter.ConvertFromString("#FFFFF056");
            drawingAttributes.IsHighlighter = true; //Highlighters are not pens
        }

        //Green highlighter
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            currentButton = button7;
            button10.Background = currentButton.Background;
            button10.Content = "";
            var drawingAttributes = myInkCanvas.DefaultDrawingAttributes;
            drawingAttributes.Color = (Color)ColorConverter.ConvertFromString("#FF00FF68");
            drawingAttributes.IsHighlighter = true;
        }

        //Blue highlighter
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            currentButton = button8;
            button10.Background = currentButton.Background;
            button10.Content = "";
            var drawingAttributes = myInkCanvas.DefaultDrawingAttributes;
            drawingAttributes.Color = (Color)ColorConverter.ConvertFromString("#FF00FFDC");
            drawingAttributes.IsHighlighter = true;
        }

        //Erase by point button
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            currentButton = button9;
            button10.Background = currentButton.Background;
            button10.Content = "Erase by\n  Point";
            myInkCanvas.DefaultDrawingAttributes.IsHighlighter = false;
        }

        //Erase by stroke button
        private void eraseByStrokeButton_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            currentButton = eraseByStrokeButton;
            button10.Background = currentButton.Background;
            button10.Content = "Erase by\n Stroke";
            myInkCanvas.DefaultDrawingAttributes.IsHighlighter = false;
        }

        //Lasso button
        private void lassoButton_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.EditingMode = InkCanvasEditingMode.Select; //Sets the editing mode to select
            currentButton = lassoButton;
            button10.Background = currentButton.Background;
            button10.Content = "Lasso\n Tool";
            myInkCanvas.DefaultDrawingAttributes.IsHighlighter = false; //Lasso is not a pen or highlighter
        }

        //Shows current color
        private void button10_Click(object sender, RoutedEventArgs e)
        {
            //Do nothing, this button displays the current button
        }

        //Save button
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog op = new SaveFileDialog();
            op.Title = "Save Ink";
            op.Filter = "Ink Serialized Format (*.isf)|*.isf"; //Save the file as an isf file
            if (op.ShowDialog() == true)
            {
                var fs = new FileStream(op.FileName, FileMode.Create);
                if (op.FileName.Contains(".isf"))
                {
                    myInkCanvas.Strokes.Save(fs);
                }
                fs.Dispose();
            }
        }

        //Load Ink
        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select Ink";
            op.Filter = "Ink Serialized Format (*.isf)|*.isf";
            if (op.ShowDialog() == true)
            {
                var fs = new FileStream(op.FileName, FileMode.Open);
                StrokeCollection strokes = new StrokeCollection(fs);
                myInkCanvas.Strokes = strokes;
                fs.Dispose();
            }
        }

        //Load Background
        private void buttonLoadBackground_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a Picture"; //I prefer sunsets and flowers as my background
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                image1.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        //Clear button
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.Strokes.Clear();
            myTextbox.Text = "";
        }

        //Exit button
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Change the brush size with slider
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (myInkCanvas != null)
            {
                var drawingAttributes = myInkCanvas.DefaultDrawingAttributes;
                Double newSize = Math.Round(slider.Value, 0);
                drawingAttributes.Width = newSize;
                drawingAttributes.Height = newSize;
            }
        }

        //Turn on graph paper
        private void graphPaperButton_Click(object sender, RoutedEventArgs e)
        {
            if (usingGraphPaper == false)
            {
                usingGraphPaper = true;
                graphPaper.Opacity = 100;
            }
            else
            {
                usingGraphPaper = false;
                graphPaper.Opacity = 0;
            }
        }

        //Gestures
        private void myInkCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            System.Windows.Ink.ApplicationGesture topGesture = e.GetGestureRecognitionResults()[0].ApplicationGesture;
            char[] findValue = convertToText(sender, e);
            if (topGesture == System.Windows.Ink.ApplicationGesture.ScratchOut) //Only scratchout gesture is currently enabled
            {
                StrokeCollection strokesToDelete = myInkCanvas.Strokes.HitTest(e.Strokes.GetBounds(), 10);
                myInkCanvas.Strokes.Remove(strokesToDelete);
            }
            else if(topGesture == System.Windows.Ink.ApplicationGesture.Tap && findValue[findValue.Count()-1] == '=')
            {
                solveEquation();
            }
            else if (topGesture == System.Windows.Ink.ApplicationGesture.Tap)
            {
                makeText();
            }
            else
            {
                e.Cancel = true;
            }
        }

        //Convert what is on the inkCanvas to text
        public void makeText()
        {
            StrokeCollection myStrokeCollection = myInkCanvas.Strokes; //Retrieve selected strokes: GetSelectedStrokes()
            using (MemoryStream ms = new MemoryStream())
            {
                myInkCanvas.Strokes.Save(ms);
                var ink = new Ink();
                ink.Load(ms.ToArray());

                using (RecognizerContext myRecognizer = new RecognizerContext())
                {
                    if (myStrokeCollection.Count != 0)
                    {
                        RecognitionStatus status;
                        myRecognizer.Strokes = ink.Strokes;
                        var recoResult = myRecognizer.Recognize(out status); //Recognize yourself!

                        if (status == RecognitionStatus.NoError)
                        {
                            //MessageBox.Show(recoResult.TopString, "Converted Text", MessageBoxButton.OK, MessageBoxImage.None); //Show converted text
                            myTextbox.Text = myTextbox.Text + "\n" + (recoResult.TopString);
                            //myStrokeCollection.Clear();
                        }
                        else
                        {
                            MessageBox.Show("You screwed up: " + status.ToString(), "Text Error", MessageBoxButton.OK, MessageBoxImage.None); //Hope you don't get this message
                        }
                    }
                    else
                    {
                        MessageBox.Show("Write something please. This is coming out of your pay!", "Text Error", MessageBoxButton.OK, MessageBoxImage.None);
                    }
                }
            }
            myInkCanvas.EditingMode = InkCanvasEditingMode.InkAndGesture;
        }

        //Solve what is on the inkCanvas
        public void solveEquation()
        {
            StrokeCollection myStrokeCollection = myInkCanvas.Strokes; //Retrieve selected strokes
            using (MemoryStream ms = new MemoryStream())
            {
                myInkCanvas.Strokes.Save(ms);
                var ink = new Ink();
                ink.Load(ms.ToArray());

                using (RecognizerContext myRecognizer = new RecognizerContext())
                {
                    if (myStrokeCollection.Count != 0)
                    {
                        RecognitionStatus status;
                        myRecognizer.Strokes = ink.Strokes;
                        var recoResult = myRecognizer.Recognize(out status); //Recognize yourself!
                        String recoResultString = recoResult.ToString();
                        char[] recoResultChar = recoResultString.ToCharArray();

                        if (status == RecognitionStatus.NoError) //Real error found
                        {
                            bool noLetter = true;
                            bool noEqual = true;
                            bool didTrig = false;
                            string stringResult = "";
                            for (int i = 0; i < recoResultChar.Count(); i++)
                            {
                                if (Char.IsLetter(recoResultChar[i])) //catch letters
                                {
                                    if (recoResultChar[i].Equals('s') && recoResultChar[i + 1].Equals('i') && recoResultChar[i + 2].Equals('n')) //solve sin
                                    {
                                        double findSin = computeSin(recoResultChar, i);
                                        stringResult = stringResult + findSin.ToString();
                                        didTrig = true;
                                        i = i + 2 + trigSize;
                                    }
                                    if (recoResultChar[i].Equals('c') && recoResultChar[i + 1].Equals('o') && recoResultChar[i + 2].Equals('s')) //solve sin
                                    {
                                        double findCos = computeCos(recoResultChar, i);
                                        stringResult = stringResult + findCos.ToString();
                                        didTrig = true;
                                        i = i + 2 + trigSize;
                                    }
                                    if (recoResultChar[i].Equals('t') && recoResultChar[i + 1].Equals('a') && recoResultChar[i + 2].Equals('n')) //solve sin
                                    {
                                        double findTan = computeTan(recoResultChar, i);
                                        stringResult = stringResult + findTan.ToString();
                                        didTrig = true;
                                        i = i + 2 + trigSize;
                                    }
                                    else
                                    {
                                        noLetter = false;
                                    }
                                }
                                else if (recoResultChar[i].Equals('(') | recoResultChar[i].Equals(')'))
                                {
                                    noLetter = false;
                                }
                                else if (recoResultChar[i] == '=' && recoResultChar.Count() == 1)
                                {
                                    noLetter = false;
                                    noEqual = false;
                                }
                                else if (recoResultChar[i] == '=')
                                {
                                    //do nothing
                                }
                                else if (didTrig && recoResultChar[i] != '=')
                                {
                                    stringResult = stringResult + recoResultChar[i].ToString();
                                }
                                else
                                {
                                    stringResult = stringResult + recoResultChar[i].ToString();
                                }
                            }
                            char[] finalResult = new char[stringResult.Count()];
                            for (int i = 0; i < stringResult.Count(); i++)
                            {
                                finalResult[i] = (char)stringResult[i];
                            }

                            if ((noLetter && noEqual) | (noEqual && didTrig))
                            {
                                if (finalResult[0].Equals('-'))
                                {
                                    char[] finalResult2 = new char[finalResult.Count() + 1];
                                    finalResult2[0] = '0';
                                    for (int i = 0; i < finalResult.Count(); i++)
                                    {
                                        finalResult2[i + 1] = finalResult[i];
                                    }
                                    double result = evalExpression(finalResult2);
                                    //MessageBox.Show(result.ToString(), "Solved Equation", MessageBoxButton.OK, MessageBoxImage.None); //Show solution
                                    myTextbox.Text = myTextbox.Text + "\n" + (recoResult.TopString + result.ToString());
                                    myStrokeCollection.Clear();
                                }
                                else
                                {
                                    double result = evalExpression(finalResult);
                                    //MessageBox.Show(result.ToString(), "Solved Equation", MessageBoxButton.OK, MessageBoxImage.None); //Show solution
                                    myTextbox.Text = myTextbox.Text + "\n" + (recoResult.TopString + result.ToString());
                                    myStrokeCollection.Clear();
                                }
                            }
                            else if (!noLetter)
                            {
                                MessageBox.Show("Found a letter in your equation. Try again or get better handwritting!", "Solve Error: Found a letter", MessageBoxButton.OK, MessageBoxImage.None);
                            }
                            else
                            {
                                MessageBox.Show("Wanna try writing an equation before that equal sign? Yeah, didn't think I'd catch that did you?!", "Solve Error: No Equation", MessageBoxButton.OK, MessageBoxImage.None);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You screwed up: " + status.ToString(), "Solve Error: You're an idiot", MessageBoxButton.OK, MessageBoxImage.None); //Hope you don't get this message
                        }
                    }
                }
            }
            myInkCanvas.EditingMode = InkCanvasEditingMode.InkAndGesture;
        }

        //Evaluate the given expression
        public static double evalExpression(char[] expr)
        {
            return parseExpression(expr, 0);
        }

        //Parse the expression
        private static double parseExpression(char[] expr, int index)
        {
            double x = parseFactors(expr, ref index);
            while (true)
            {
                char op = expr[index];
                if (op != '+' && op != '-')
                    return x;
                index++;
                if(index == expr.Count()) //catch improper equations
                {
                    return x;
                }
                if(expr[index] == '-') //catch double negative
                {
                    index++;
                    op = '+';
                }
                double y = parseFactors(expr, ref index);
                if (op == '+')
                    x += y;
                else
                    x -= y;
            }
        }

        //Parse factors
        private static double parseFactors(char[] expr, ref int index)
        {
            double x = GetDouble(expr, ref index);
            while (true)
            {
                char op = expr[index];
                if (op != '/' && op != '*')
                    return x;
                index++;
                double y = GetDouble(expr, ref index);
                if (op == '/')
                    x /= y;
                else
                    x *= y;
            }
        }

        //Get the next double
        private static double GetDouble(char[] expr, ref int index)
        {
            string dbl = "";
            while (((int)expr[index] >= 48 && (int)expr[index] <= 57) || expr[index] == 46)
            {
                dbl = dbl + expr[index].ToString();
                index++;
                if (index == expr.Length)
                {
                    index--;
                    break;
                }
            }
            return double.Parse(dbl);
        }

        //Convert Selected to Text
        public char[] convertToText(object sender, InkCanvasGestureEventArgs e)
        {
            StrokeCollection myStrokeCollection = myInkCanvas.Strokes; //Retrieve selected strokes
            using (MemoryStream ms = new MemoryStream())
            {
                myInkCanvas.Strokes.Save(ms);
                var ink = new Ink();
                ink.Load(ms.ToArray());
                char[] noChar = new char[1];
                using (RecognizerContext myRecognizer = new RecognizerContext())
                {
                    if (myStrokeCollection.Count != 0)
                    {
                        RecognitionStatus status;
                        myRecognizer.Strokes = ink.Strokes;
                        var recoResult = myRecognizer.Recognize(out status); //Recognize yourself!
                        String recoResultString = recoResult.ToString();
                        char[] recoResultChar = recoResultString.ToCharArray();
                        if (status == RecognitionStatus.NoError)
                        {
                            return recoResultChar;
                        }
                        else
                        {
                            MessageBox.Show("You screwed up: " + status.ToString(), "Text Error", MessageBoxButton.OK, MessageBoxImage.None); //Hope you don't get this message
                        }
                    }
                }
                return noChar;
            }
        }
        
        //Compute Sin
        public double computeSin(char[] charArray, int i)
        {
            double mySin = 0;
            if (charArray[i+4].Equals('=') | charArray[i + 4].Equals(')') | !Char.IsNumber(charArray[i + 4]))
            {
                string numInSin = charArray[i + 3].ToString();
                double inSin = Double.Parse(numInSin);
                double sinX = ((Math.PI / 180) * inSin);
                sinX = Math.Sin(sinX);
                sinX = Math.Round(sinX, 4);
                trigSize = 1;
                mySin = sinX;
            }
            else if(charArray[i + 5].Equals('=') | charArray[i + 5].Equals(')') | !Char.IsNumber(charArray[i + 5]))
            {
                string num1 = charArray[i + 3].ToString();
                string num2 = charArray[i + 4].ToString();
                string numInSin = num1 + num2;
                double inSin = Double.Parse(numInSin);
                double sinX = ((Math.PI / 180) * inSin);
                sinX = Math.Sin(sinX);
                sinX = Math.Round(sinX, 4);
                trigSize = 2;
                mySin = sinX;
            }
            else
            {
                string num1 = charArray[i + 3].ToString();
                string num2 = charArray[i + 4].ToString();
                string num3 = charArray[i + 5].ToString();
                string numInSin = num1 + num2 + num3;
                double inSin = Double.Parse(numInSin);
                double sinX = ((Math.PI / 180) * inSin);
                sinX = Math.Sin(sinX);
                sinX = Math.Round(sinX, 4);
                trigSize = 3;
                mySin = sinX;
            }
            return mySin;
        }

        //Compute Cos
        public double computeCos(char[] charArray, int i)
        {
            double myCos = 0;
            if (charArray[i + 4].Equals('=') | charArray[i + 4].Equals(')') | !Char.IsNumber(charArray[i + 4]))
            {
                string numInCos = charArray[i + 3].ToString();
                double inCos = Double.Parse(numInCos);
                double cosX = ((Math.PI / 180) * inCos);
                cosX = Math.Cos(cosX);
                cosX = Math.Round(cosX, 4);
                trigSize = 1;
                myCos = cosX;
            }
            else if (charArray[i + 5].Equals('=') | charArray[i + 5].Equals(')') | !Char.IsNumber(charArray[i + 5]))
            {
                string num1 = charArray[i + 3].ToString();
                string num2 = charArray[i + 4].ToString();
                string numInCos = num1 + num2;
                double inCos = Double.Parse(numInCos);
                double cosX = ((Math.PI / 180) * inCos);
                cosX = Math.Cos(cosX);
                cosX = Math.Round(cosX, 4);
                trigSize = 2;
                myCos = cosX;
            }
            else
            {
                string num1 = charArray[i + 3].ToString();
                string num2 = charArray[i + 4].ToString();
                string num3 = charArray[i + 5].ToString();
                string numInCos = num1 + num2 + num3;
                double inCos = Double.Parse(numInCos);
                double cosX = ((Math.PI / 180) * inCos);
                cosX = Math.Cos(cosX);
                cosX = Math.Round(cosX, 4);
                trigSize = 3;
                myCos = cosX;
            }
            return myCos;
        }

        //Compute Tan
        public double computeTan(char[] charArray, int i)
        {
            double myTan = 0;
            if (charArray[i + 4].Equals('=') | charArray[i + 4].Equals(')') | !Char.IsNumber(charArray[i + 4]))
            {
                string numInTan = charArray[i + 3].ToString();
                double inTan = Double.Parse(numInTan);
                double tanX = ((Math.PI / 180) * inTan);
                tanX = Math.Tan(tanX);
                tanX = Math.Round(tanX, 4);
                trigSize = 1;
                myTan = tanX;
            }
            else if (charArray[i + 5].Equals('=') | charArray[i + 5].Equals(')') | !Char.IsNumber(charArray[i + 5]))
            {
                string num1 = charArray[i + 3].ToString();
                string num2 = charArray[i + 4].ToString();
                string numInTan = num1 + num2;
                double inTan = Double.Parse(numInTan);
                double tanX = ((Math.PI / 180) * inTan);
                tanX = Math.Tan(tanX);
                tanX = Math.Round(tanX, 4);
                trigSize = 2;
                myTan = tanX;
            }
            else
            {
                string num1 = charArray[i + 3].ToString();
                string num2 = charArray[i + 4].ToString();
                string num3 = charArray[i + 5].ToString();
                string numInTan = num1 + num2 + num3;
                double inTan = Double.Parse(numInTan);
                double tanX = ((Math.PI / 180) * inTan);
                tanX = Math.Tan(tanX);
                tanX = Math.Round(tanX, 4);
                trigSize = 3;
                myTan = tanX;
            }
            return myTan;
        }
    }
}
