using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ImageUtils.Utils
{
    public static class LayerSplitter
    {
        private class Functions
        {
            public void ByBrightness(int? layers = null)
            {
                Project.LayerManager.Layers.Clear(); // Clear the layers

                //int _splitBy = (int)(100 / (layers == null ? 10 : layers));
                int _splitBy = (int)(layers == null ? 10 : layers);

                for (int i = 0; i < _splitBy; i++)
                    Project.LayerManager.Layers.Add(new Project.Layer() { LayerLevel = i, Name = $"{i + 1}", Content = new Bitmap(Globals.mainWindow.bm.Width, Globals.mainWindow.bm.Height), Selected = false, Visible = true });

                Events.RaiseLayerUpdate(EventArgs.Empty);

                Task.Factory.StartNew(async () =>
                {
                    Bitmap _bm = new Bitmap(Globals.mainWindow.bm);
                    List<Project.Layer> Layers = new List<Project.Layer>(Project.LayerManager.Layers);

                    for (int y = 0; y < _bm.Height; y++)
                    {
                        string b = "";

                        for (int x = 0; x < _bm.Width; x++)
                        {
                            float Brightness = _bm.GetPixel(x, y).GetBrightness();
                            b += Brightness.ToString() + " ";

                            bool done = false;

                            for (int i = 0; i < _splitBy; i++)
                            {
                                if (done) continue;

                                if (Brightness * 100 <= i * 10)
                                {
                                    try
                                    {
                                        Layers[_splitBy - i].Content.SetPixel(x, y, _bm.GetPixel(x, y));
                                    }
                                    catch
                                    {
                                        Layers[0].Content.SetPixel(x, y, _bm.GetPixel(x, y));
                                    }
                                    done = true;
                                }
                            }

                            if (!done) Layers[0].Content.SetPixel(x, y, _bm.GetPixel(x, y));
                        }

                        await Task.Delay(10);
                    }
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Project.LayerManager.Layers = new List<Project.Layer>(Layers);

                        Events.RaiseLayerUpdate(EventArgs.Empty);
                    });
                });
            }
        }
        public static void ByBrightness(int? layers = null)
        {
            Project.LayerManager.Layers.Clear(); // Clear the layers

            //int _splitBy = (int)(100 / (layers == null ? 10 : layers));
            int _splitBy = (int)(layers == null ? 10 : layers);

            for(int i = 0; i < _splitBy; i++)
                Project.LayerManager.Layers.Add(new Project.Layer() { LayerLevel = i, Name = $"{i + 1}", Content = new Bitmap(Globals.mainWindow.bm.Width, Globals.mainWindow.bm.Height), Selected = false, Visible = true });

            Events.RaiseLayerUpdate(EventArgs.Empty);

            Windows.ProgressWindow progressWindow = new Windows.ProgressWindow();
            progressWindow.Show();
            progressWindow.Update(false, "Splitting the layers", "Working on it...");

            Task.Factory.StartNew(async () =>
            {
                Bitmap _bm = new Bitmap(Globals.mainWindow.bm);
                List<Project.Layer> Layers = new List<Project.Layer>(Project.LayerManager.Layers);

                for (int y = 0; y < _bm.Height; y++)
                {
                    string b = "";

                    for (int x = 0; x < _bm.Width; x++)
                    {
                        float Brightness = _bm.GetPixel(x, y).GetBrightness();
                        b += Brightness.ToString() + " ";

                        bool done = false;

                        for (int i = 0; i < _splitBy; i++)
                        {
                            if (done) continue;

                            if (Brightness * 100 <= i * 10)
                            {
                                try
                                {
                                    Layers[_splitBy - i].Content.SetPixel(x, y, _bm.GetPixel(x, y));
                                }
                                catch
                                {
                                    Layers[0].Content.SetPixel(x, y, _bm.GetPixel(x, y));
                                }
                                done = true;
                            }
                        }

                        if (!done) Layers[0].Content.SetPixel(x, y, _bm.GetPixel(x, y));
                    }

                    //await Task.Delay(10);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        progressWindow.Update(y, _bm.Height, null, $"Working on it... {y}/{_bm.Height}");
                    });
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Project.LayerManager.Layers = new List<Project.Layer>(Layers);

                    Events.RaiseLayerUpdate(EventArgs.Empty);

                    progressWindow.Close();
                });
            });

            /*for (int y = 0; y < _bm.Height; y++)
            {
                string b = "";

                for (int x = 0; x < _bm.Width; x++)
                {
                    float Brightness = _bm.GetPixel(x, y).GetBrightness();
                    b += Brightness.ToString() + " ";

                    bool done = false;

                    for (int i = 0; i < _splitBy; i++)
                    {
                        if (done) continue;

                        if(Brightness * 100 <= i * 10)
                        {
                            try
                            {
                                Project.LayerManager.Layers[_splitBy - i].Content.SetPixel(x, y, _bm.GetPixel(x, y));
                            } catch
                            {
                                Project.LayerManager.Layers[0].Content.SetPixel(x, y, _bm.GetPixel(x, y));
                            }
                            done = true;
                        }
                    }

                    if (!done) Project.LayerManager.Layers[0].Content.SetPixel(x, y, _bm.GetPixel(x, y));
                }

                //MessageBox.Show(b.TrimEnd());
            }

            Events.RaiseLayerUpdate(EventArgs.Empty);*/
        }
    }
}
