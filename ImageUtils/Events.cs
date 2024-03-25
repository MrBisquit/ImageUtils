using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUtils
{
    public static class Events
    {
        public delegate void LayerUpdateEventHandler(object? sender, EventArgs args);
        public static event LayerUpdateEventHandler LayerUpdate;

        public static void RaiseLayerUpdate(EventArgs e)
        {
            LayerUpdate?.Invoke(null, e);
        }

        public delegate void UpdatePreviewEventHandler(object? sender, UpdatePreviewEventArgs args);
        public static event UpdatePreviewEventHandler UpdatePreview;

        public static void RaiseUpdatePreview(UpdatePreviewEventArgs e)
        {
            UpdatePreview?.Invoke(null, e);
        }

        public class UpdatePreviewEventArgs
        {
            public Bitmap NewPreview;
        }
    }
}
