using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DesktopAnnotator.Settings
{
    class SettingWindowViewModel : BindableBase
    {

        #region ペンの設定
        public int PenThickness
        {
            get { return Properties.Settings.Default.PenThickness; }
            set
            {
                if (Properties.Settings.Default.PenThickness != value)
                {
                    Properties.Settings.Default.PenThickness = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color PenColor
        {
            get { var col = Properties.Settings.Default.PenColor; return Color.FromRgb(col.R, col.G, col.B); }
            set
            {
                if (PenColor != value)
                {
                    Properties.Settings.Default.PenColor = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region マーカーの設定
        public int MarkerThickness
        {
            get { return Properties.Settings.Default.MarkerThickness; }
            set
            {
                if (Properties.Settings.Default.MarkerThickness != value)
                {
                    Properties.Settings.Default.MarkerThickness = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color MarkerColor
        {
            get { var col = Properties.Settings.Default.MarkerColor; return Color.FromRgb(col.R, col.G, col.B); }
            set
            {
                if (MarkerColor != value)
                {
                    Properties.Settings.Default.MarkerColor = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region 背景の設定
        public Color BackgroundColor
        {
            get { var col = Properties.Settings.Default.BackgroundColor; return Color.FromRgb(col.R, col.G, col.B); }
            set
            {
                if (BackgroundColor != value)
                {
                    Properties.Settings.Default.BackgroundColor = CreateColor(value, this.BackgroundTransparency);
                    OnPropertyChanged();
                }
            }
        }

        public int BackgroundTransparency
        {
            get { var a = Properties.Settings.Default.BackgroundColor.A; return (int)(a / 255.0 * 100); }
            set
            {
                if (BackgroundTransparency != value)
                {
                    Properties.Settings.Default.BackgroundColor = CreateColor(this.BackgroundColor, value);
                    OnPropertyChanged();
                }
            }
        }

        private Color CreateColor(Color col, int alpha)
        {
            return Color.FromArgb((byte)(alpha / 100.0 * 255), col.R, col.G, col.B);
        }
        #endregion


        public void Save()
        {
            Properties.Settings.Default.Save();
        }

        public void Reload()
        {
            Properties.Settings.Default.Reload();
        }

        private Color? SelectColor()
        {
            var cd = new System.Windows.Forms.ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var color = Color.FromArgb(cd.Color.A,
                                           cd.Color.R,
                                           cd.Color.G,
                                           cd.Color.B);
                return color;
            }

            return null;
        }

        #region コマンドの実装
        private DelegateCommand selectPenColorCommand;
        public DelegateCommand SelectPenColorCommand
        {
            get { return selectPenColorCommand = selectPenColorCommand ?? new DelegateCommand(SelectPenColor); }
        }

        private void SelectPenColor(object param)
        {
            var result = SelectColor();
            if (result != null)
            {
                PenColor = result.Value;
            }
        }

        private DelegateCommand selectMarkerColorCommand;
        public DelegateCommand SelectMarkerColorCommand
        {
            get { return selectMarkerColorCommand = selectMarkerColorCommand ?? new DelegateCommand(SelectMarkerColor); }
        }

        private void SelectMarkerColor(object param)
        {
            var result = SelectColor();
            if (result != null)
            {
                MarkerColor = result.Value;
            }
        }

        private DelegateCommand selectBackgroundColorCommand;
        public DelegateCommand SelectBackgroundColorCommand
        {
            get { return selectBackgroundColorCommand = selectBackgroundColorCommand ?? new DelegateCommand(SelectBackgroundColor); }
        }

        private void SelectBackgroundColor(object param)
        {
            var result = SelectColor();
            if (result != null)
            {
                BackgroundColor = result.Value;
            }
        }

        private DelegateCommand saveSettingsCommand;
        public DelegateCommand SaveSettingsCommand
        {
            get { return saveSettingsCommand = saveSettingsCommand ?? new DelegateCommand(SaveSettings); }
        }

        private void SaveSettings(object param)
        {
            // 設定を保存する
            Properties.Settings.Default.Save();
        }

        private DelegateCommand cancelSettingsCommand;
        public DelegateCommand CancelSettingsCommand
        {
            get { return cancelSettingsCommand = cancelSettingsCommand ?? new DelegateCommand(CancelSettings); }
        }

        private void CancelSettings(object param)
        {
            // 変更をキャンセルする
            Properties.Settings.Default.Reload();
        }

        #endregion

    }
}
