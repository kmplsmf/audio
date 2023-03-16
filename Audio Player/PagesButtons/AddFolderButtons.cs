using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Audio_Player
{

    public partial class MainWindow : Window
    {

        private void RemovePath_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить этот путь?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (mainPL.AudioList.SequenceEqual(CurrentList))
                {
                    string r = ((Button)sender).Content.ToString(); 
                    mainPL.AudioList = mainPL.AudioList.Where(x => x.DirectoryName != r).ToList(); 
                    Paths.Remove(((Button)sender).Content.ToString()); 
                    ListPaths.ItemsSource = new List<string>(Paths); 
                    Play.ItemsSource = mainPL.AudioList; 
                    CurrentList = mainPL.AudioList; 
                    CurrentIndex = 0; 
                }
                else
                {
                    string r = ((Button)sender).Content.ToString(); 
                    mainPL.AudioList = mainPL.AudioList.Where(x => x.DirectoryName != r).ToList();
                    Paths.Remove(((Button)sender).Content.ToString());
                    ListPaths.ItemsSource = new List<string>(Paths);
                }
                mainPL.GetTime();
            }
        }

        private void AddPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog Dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (Dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!Paths.Contains(Dialog.SelectedPath)) 
                {
                    Paths.Add(Dialog.SelectedPath); 
                    ListPaths.ItemsSource = new List<string>(Paths); 
                    if (mainPL.AudioList.SequenceEqual(CurrentList)) 
                    {
                        mainPL.AudioList.AddRange(load_audios.GetMusic(Dialog.SelectedPath)); 
                        Play.ItemsSource = new List<Audio>(mainPL.AudioList);
                        CurrentList = mainPL.AudioList; 
                        CurrentIndex = 0; 
                    }
                    else
                    {
                        mainPL.AudioList.AddRange(load_audios.GetMusic(Dialog.SelectedPath));  
                    }
                    mainPL.GetTime();
                }
                else
                {
                    MessageBox.Show("Такой путь уже существует");
                }
            }
        }

    }

}