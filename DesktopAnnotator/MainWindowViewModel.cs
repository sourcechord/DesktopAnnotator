using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Data;
using System.Windows.Media;

namespace DesktopAnnotator
{
    /// <summary>
    /// 描画モードの種類
    /// </summary>
    public enum DrawingMode
    {
        None = 0,
        Pen,
        Marker,
        Eraser,
        EraseByStroke
    }

    
    public class MainWindowViewModel : BindableBase
    {
        public DrawingAttributes PenAttributes
        {
            get
            {
                return new DrawingAttributes()
                {
                    Color = Properties.Settings.Default.PenColor,
                    Width = Properties.Settings.Default.PenThickness,
                    Height = Properties.Settings.Default.PenThickness
                };
            }
        }

        public DrawingAttributes MarkerAttributes
        {
            get
            {
                return new DrawingAttributes()
                {
                    Color = Properties.Settings.Default.MarkerColor,
                    Height = Properties.Settings.Default.MarkerThickness,
                    IsHighlighter = true,
                    IgnorePressure = true,
                };
            }
        }

        public DrawingAttributes EraserAttributes
        {
            get
            {
                return new DrawingAttributes() {};
            }
        }

        private DrawingMode currentMode;
        /// <summary>
        /// 現在の描画モードを取得または設定します。
        /// </summary>
        public DrawingMode CurrentMode
        {
            get { return currentMode; }
            set
            {
                // プロパティ値を変更
                this.SetProperty(ref this.currentMode, value);
                // 設定されたモードに合わせて、他のプロパティも設定
                switch (currentMode)
                {
                    case DrawingMode.None:
                        break;
                    case DrawingMode.Pen:
                        EditingMode = InkCanvasEditingMode.Ink;
                        DrawingAttribute = PenAttributes;
                        break;
                    case DrawingMode.Marker:
                        EditingMode = InkCanvasEditingMode.Ink;
                        DrawingAttribute = MarkerAttributes;
                        break;
                    case DrawingMode.Eraser:
                        EditingMode = InkCanvasEditingMode.EraseByPoint;
                        DrawingAttribute = EraserAttributes;
                        break;
                    case DrawingMode.EraseByStroke:
                        EditingMode = InkCanvasEditingMode.EraseByStroke;
                        DrawingAttribute = EraserAttributes;
                        break;
                    default:
                        break;
                }
            }
        }


        private InkCanvasEditingMode editingMode;
        /// <summary>
        /// InkCanvasの編集モードを取得または設定します。
        /// </summary>
        public InkCanvasEditingMode EditingMode
        {
            get { return editingMode; }
            set { this.SetProperty(ref this.editingMode, value); }
        }

        private DrawingAttributes drawingAttribute;
        /// <summary>
        /// 描画に使用する属性を取得または設定します。
        /// </summary>
        public DrawingAttributes DrawingAttribute
        {
            get { return drawingAttribute; }
            set { this.SetProperty(ref this.drawingAttribute, value); }
        }

        private StrokeCollection strokes;
        /// <summary>
        /// InkCanvasの描画内容を取得または設定します。
        /// </summary>
        public StrokeCollection Strokes
        {
            get { return strokes; }
            set { this.SetProperty(ref this.strokes, value); }
        }


        public MainWindowViewModel()
        {
            CurrentMode = DrawingMode.Pen;
            Strokes = new StrokeCollection();
        }

        #region 各種コマンドの実装
        private DelegateCommand clearCommand;
        public DelegateCommand ClearCommand
        {
            get { return clearCommand = clearCommand ?? new DelegateCommand(ClearCanvas); }
        }

        private void ClearCanvas(object param)
        {
            Strokes.Clear();
        }


        private DelegateCommand selectDrawingModeCommand;
        public DelegateCommand SelectDrawingModeCommand
        {
            get { return selectDrawingModeCommand = selectDrawingModeCommand ?? new DelegateCommand(SelectDrawingMode); }
        }

        private void SelectDrawingMode(object param)
        {
            var mode = (DrawingMode)param;
            CurrentMode = mode;
        }

        #endregion
    }

    /// <summary>
    /// 現在の描画モードと指定されたパラメータからbool値に変換するコンバーター
    /// </summary>
    /// <remarks>
    /// ContextMenu内のチェック表示に使用
    /// </remarks>
    public class CurrentModeToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var p = (DrawingMode)parameter;
            var v = (DrawingMode)value;

            return p == v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
