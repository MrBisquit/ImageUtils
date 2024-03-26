using ImageUtils.Project;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUtils.Dialogs
{
    public static class Confirm
    {
        public static bool ConfirmLayerDeletion(Layer layer) => ConfirmLayerDeletion([layer]);
        public static bool ConfirmLayerDeletion(Layer[] layers)
        {
            string list = "";
            foreach(var layer in layers)
            {
                list += $"- {layer.Name}\n";
            }

            TaskDialog td = new TaskDialog()
            {
                WindowTitle = "ImageUtils - Confirm layer deletion",
                MainInstruction = "Confirm layer deletion",
                Content = "Are you sure you'd like to delete the following layer(s)" +
                list
            };

            TaskDialogButton Yes = new TaskDialogButton(ButtonType.Yes);
            TaskDialogButton No = new TaskDialogButton(ButtonType.No);

            td.Buttons.Add(Yes);
            td.Buttons.Add(No);

            TaskDialogButton result = td.ShowDialog();

            return result == Yes;
        }
    }
}
