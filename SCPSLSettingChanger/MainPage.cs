using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCPSLSettingChanger
{
    public partial class FRM_MainPage : Form
    {
        string[] LanguageValues =
            { "ca", "cs", "da", "de", "de-AT", "English (default)", "es", "fi", "fr", "fr-CA", "hu", "it", "ja",
            "ko", "lv", "nb" ,"nl", "pl", "pt" ,"pt-BR", "ru", "sk", "th", "tr", "uk", "zh-Hans", "zh-Hant"};
        int[] Framerates = { -1, 15, 30, 45, 60, 90, 120, 144, 165, 240 };
        public FRM_MainPage()
        {
            InitializeComponent();
        }

        private void FRM_MainPage_Load(object sender, EventArgs e)
        {
            LoadValues();
            if (CHK_VSync.Checked)
                CBOX_FPSLimit.Enabled = false;
            else
                CBOX_FPSLimit.Enabled = true;
            if (CHK_Shadows.Checked)
            {
                CBOX_ShadowResolution.Enabled = true;
                CBOX_ShadowDistance.Enabled = true;
            }
            else
            {
                CBOX_ShadowResolution.Enabled = false;
                CBOX_ShadowDistance.Enabled = false;
            }
        }

        private void FRM_MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveValues();
        }

        private void BTN_GenerateConfig_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SCP Secret Laboratory")))
            {
                PlayerPrefsSl.Set("gammavalue", float.Parse(TBOX_Brightness.Text) * 0.02f);
                if ((float.Parse(TBOX_Sensitivty.Text) * 0.03f) >= 0.1f)
                    PlayerPrefsSl.Set("Sens", float.Parse(TBOX_Sensitivty.Text) * 0.03f);
                else
                    PlayerPrefsSl.Set("Sens", 0.1f);
                PlayerPrefsSl.Set("y_invert", CHK_InvertYAxisChoice.Checked);
                PlayerPrefsSl.Set("SavedResolutionSet", CBOX_ScreenResolution.SelectedIndex);
                PlayerPrefsSl.Set("ScreenMode", CBOX_WindowMode.SelectedIndex);
                if (CHK_VSync.Checked)
                {
                    PlayerPrefsSl.Set("gfxsets_vsync", CHK_VSync.Checked);
                    PlayerPrefsSl.Set("MaxFramerate", -1);
                }
                else
                {
                    PlayerPrefsSl.Set("gfxsets_vsync", CHK_VSync.Checked);
                    PlayerPrefsSl.Set("MaxFramerate", Framerates[CBOX_FPSLimit.SelectedIndex]);
                }
                PlayerPrefsSl.Set("gfxsets_textures", CBOX_TextureQuality.SelectedIndex);
                PlayerPrefsSl.Set("gfxsets_maxblood", CBOX_MaxBlood.SelectedIndex);
                if (CHK_Shadows.Checked) {
                    PlayerPrefsSl.Set("gfxsets_shadows", CHK_Shadows.Checked);
                    PlayerPrefsSl.Set("gfxsets_shadres", CBOX_ShadowResolution.SelectedIndex);
                    PlayerPrefsSl.Set("gfxsets_shaddis_new", CBOX_ShadowDistance.SelectedIndex);
                }
                else
                    PlayerPrefsSl.Set("gfxsets_shadows", CHK_Shadows.Checked);
                PlayerPrefsSl.Set("gfxsets_mb", CHK_VSync.Checked);
                PlayerPrefsSl.Set("gfxsets_cc", CHK_ColorCorrection.Checked);
                PlayerPrefsSl.Set("gfxsets_aa", CHK_AntiAliasing.Checked);
                PlayerPrefsSl.Set("gfxsets_hp", CHK_LightRenderingMode.Checked);
                PlayerPrefsSl.Set("AudioSettings_Master", float.Parse(TBOX_MasterVolumeValue.Text) * 0.01f);
                PlayerPrefsSl.Set("AudioSettings_Effects", float.Parse(TBOX_SoundEffectsVolume.Text) * 0.01f);
                PlayerPrefsSl.Set("AudioSettings_VoiceChat", float.Parse(TBOX_VoiceChatValue.Text) * 0.01f);
                PlayerPrefsSl.Set("AudioSettings_MenuMusic", float.Parse(TBOX_MenuMusicValue.Text) * 0.01f);
                PlayerPrefsSl.Set("AudioSettings_Interface", float.Parse(TBOX_InterfaceVolumeValue.Text) * 0.01f);
                PlayerPrefsSl.Set("MenuTheme", CBOX_MenuMusicTheme.SelectedIndex);
                PlayerPrefsSl.Set("MaintainSliderProportions", CHK_SameMenuAndInterface.Checked);
                PlayerPrefsSl.Set("ClassIntroFastFade", CHK_FastIntroFade.Checked);
                PlayerPrefsSl.Set("HeadBob", CHK_HeadBob.Checked);
                PlayerPrefsSl.Set("ToggleSprint", CHK_ToggleSprint.Checked);
                PlayerPrefsSl.Set("HealthBarShowsExact", CHK_DisplayExactHPValue.Checked);
                PlayerPrefsSl.Set("RichPresence", CHK_RichPresence.Checked);
                PlayerPrefsSl.Set("PublicLobby", CHK_PublicLobby.Checked);
                PlayerPrefsSl.Set("HideIP", CHK_HideIP.Checked);
                PlayerPrefsSl.Set("ToggleSearch", CHK_ToggleSearch.Checked);
                PlayerPrefsSl.Set("ModeSwitchSetting079", CHK_SCP079ToggleView.Checked);
                PlayerPrefsSl.Set("PostProcessing079", CHK_SCP079EnablePostProcessing.Checked);
                PlayerPrefsSl.Set("translation_changed", true);
                PlayerPrefsSl.Set("translation_path", LanguageValues[CBOX_LanguageOptions.SelectedIndex]);
                PlayerPrefsSl.Set("menumode", CBOX_MenuOptions.SelectedIndex);
                PlayerPrefsSl.Set("DisplaySteamProfile", CHK_DisplaySteamProfile.Checked);
                PlayerPrefsSl.Set("DNT", CHK_DoNotTrack.Checked);
                PlayerPrefsSl.Set("W_2_0", CBOX_E11Sight.SelectedIndex);
                PlayerPrefsSl.Set("W_2_1", CBOX_E11Barrel.SelectedIndex);
                PlayerPrefsSl.Set("W_2_2", CBOX_E11Other.SelectedIndex);
                PlayerPrefsSl.Set("W_1_0", CBOX_Project90Sight.SelectedIndex);
                PlayerPrefsSl.Set("W_1_1", CBOX_Project90Barrel.SelectedIndex);
                PlayerPrefsSl.Set("W_1_2", CBOX_Project90Other.SelectedIndex);
                PlayerPrefsSl.Set("W_3_0", CBOX_MP7Sight.SelectedIndex);
                PlayerPrefsSl.Set("W_3_1", CBOX_MP7Barrel.SelectedIndex);
                PlayerPrefsSl.Set("W_3_2", CBOX_MP7Other.SelectedIndex);
                PlayerPrefsSl.Set("W_5_0", CBOX_USPSight.SelectedIndex);
                PlayerPrefsSl.Set("W_5_1", CBOX_USPBarrel.SelectedIndex); 
                PlayerPrefsSl.Set("W_5_2", CBOX_USPOther.SelectedIndex);
                PlayerPrefsSl.Set("W_0_0", CBOX_COM15Sight.SelectedIndex);
                PlayerPrefsSl.Set("W_0_1", CBOX_COM15Barrel.SelectedIndex);
                PlayerPrefsSl.Set("W_0_2", CBOX_COM15Other.SelectedIndex);
                MessageBox.Show("SCP:SL game settings have been changed now!", "Successfully written new file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("The game directory where the settings belong to is not there (Do you have the game installed?)", "Directory does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TBOX_MasterVolumeValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CheckInput(e, (sender as TextBox)))
                e.Handled = true;
        }

        private void TBOX_MasterVolumeValue_TextChanged(object sender, EventArgs e)
        {
            FormatInput(TBOX_MasterVolumeValue, 100.0);
        }

        private void TBOX_SoundEffectsVolume_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CheckInput(e, (sender as TextBox)))
                e.Handled = true;
        }
        private void TBOX_SoundEffectsVolume_TextChanged(object sender, EventArgs e)
        {
            FormatInput(TBOX_SoundEffectsVolume, 100.0);
        }

        private void TBOX_VoiceChatValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CheckInput(e, (sender as TextBox)))
                e.Handled = true;
        }

        private void TBOX_VoiceChatValue_TextChanged(object sender, EventArgs e)
        {
            FormatInput(TBOX_VoiceChatValue, 100.0);
        }

        private void TBOX_MenuMusicValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CheckInput(e, (sender as TextBox)))
                e.Handled = true;
        }

        private void TBOX_MenuMusicValue_TextChanged(object sender, EventArgs e)
        {
            FormatInput(TBOX_MenuMusicValue, TBOX_InterfaceVolumeValue, 100.0);
        }

        private void TBOX_InterfaceVolumeValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CheckInput(e, (sender as TextBox)))
                e.Handled = true;
        }

        private void TBOX_InterfaceVolumeValue_TextChanged(object sender, EventArgs e)
        {
            FormatInput(TBOX_InterfaceVolumeValue, TBOX_MenuMusicValue, 100.0);
        }

        private void TBOX_Brightness_TextChanged(object sender, EventArgs e)
        {
            FormatInput(TBOX_Brightness, 100.0);
        }

        private void TBOX_Brightness_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CheckInput(e, (sender as TextBox)))
                e.Handled = true;
        }

        private void TBOX_Sensitivty_TextChanged(object sender, EventArgs e)
        {
            FormatInput(TBOX_Sensitivty, 100.0);
        }

        private void TBOX_Sensitivity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CheckInput(e, (sender as TextBox)))
                e.Handled = true;
        }

        private void CHK_VSync_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_VSync.Checked)
                CBOX_FPSLimit.Enabled = false;
            else
                CBOX_FPSLimit.Enabled = true;
        }

        private void CHK_Shadows_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_Shadows.Checked)
            {
                CBOX_ShadowResolution.Enabled = true;
                CBOX_ShadowDistance.Enabled = true;
            }
            else
            {
                CBOX_ShadowResolution.Enabled = false;
                CBOX_ShadowDistance.Enabled = false;
            }
        }

        private void CHK_SameMenuAndInterface_CheckedChanged(object sender, EventArgs e)
        {
            if (double.Parse(TBOX_MenuMusicValue.Text) < double.Parse(TBOX_InterfaceVolumeValue.Text))
                TBOX_MenuMusicValue.Text = TBOX_InterfaceVolumeValue.Text;
            else
                TBOX_InterfaceVolumeValue.Text = TBOX_MenuMusicValue.Text;
        }

        private bool CheckInput(KeyPressEventArgs KeyPress, TextBox InputBox)
        {
            int count = InputBox.Text.Count(c => c == '.');
            if (KeyPress.KeyChar == '.' && count < 1)
                return false;
            if (char.IsDigit(KeyPress.KeyChar))
                return false;
            if (char.IsControl(KeyPress.KeyChar))
                return false;
            return true;
        }

        private void FormatInput(TextBox InputBox, double MaxValue)
        {
            if (String.IsNullOrWhiteSpace(InputBox.Text))
            {
                InputBox.Text = "0";
                return;
            }
            if (InputBox.Text.StartsWith("."))
                return;
            if (double.Parse(InputBox.Text) > MaxValue)
                InputBox.Text = MaxValue.ToString();
        }

        private void FormatInput(TextBox InputBox1, TextBox InputBox2, double MaxValue)
        {
            if (String.IsNullOrWhiteSpace(InputBox1.Text))
            {
                InputBox1.Text = "0";
                return;
            }
            if (double.Parse(InputBox1.Text) <= MaxValue)
            {
                double TextBox1Value = double.Parse(InputBox1.Text);
                if (CHK_SameMenuAndInterface.Checked && !String.IsNullOrWhiteSpace(InputBox1.Text))
                {
                    double TextBox2Value = double.Parse(InputBox2.Text);
                    if (TextBox2Value != TextBox1Value)
                        InputBox2.Text = TextBox1Value.ToString();
                }
            }
            else
                if (double.Parse(InputBox1.Text) > MaxValue)
                    InputBox1.Text = MaxValue.ToString();
        }

        private void SaveValues()
        {
            SCPSLSettingChanger.Properties.Settings.Default.ScreenResolutionChoice = CBOX_ScreenResolution.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.WindowModeChoice = CBOX_WindowMode.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.VSyncEnabled = CHK_VSync.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.FPSLimitChoice = CBOX_FPSLimit.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.TextureQualityChoice = CBOX_TextureQuality.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.MaxBloodAmountChoice = CBOX_MaxBlood.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.ShadowsEnabled = CHK_Shadows.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.ShadowResolutionChoice = CBOX_ShadowResolution.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.ShadowDistanceChoice = CBOX_ShadowDistance.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.MotionBlurEnabled = CHK_MotionBlur.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.ColorCorrectionEnabled = CHK_ColorCorrection.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.AntiAliasingEnabled = CHK_AntiAliasing.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.LightRenderingModeEnabled = CHK_LightRenderingMode.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.MasterVolumeValue = double.Parse(TBOX_MasterVolumeValue.Text);
            SCPSLSettingChanger.Properties.Settings.Default.SoundEffectsVolumeValue = double.Parse(TBOX_SoundEffectsVolume.Text);
            SCPSLSettingChanger.Properties.Settings.Default.VoiceChatVolumeValue = double.Parse(TBOX_VoiceChatValue.Text);
            SCPSLSettingChanger.Properties.Settings.Default.MenuMusicVolumeValue = double.Parse(TBOX_MenuMusicValue.Text);
            SCPSLSettingChanger.Properties.Settings.Default.InterfaceVolumeValue = double.Parse(TBOX_InterfaceVolumeValue.Text);
            SCPSLSettingChanger.Properties.Settings.Default.SameMenuMusicAndInterfaceVolumeValueEnabled = CHK_SameMenuAndInterface.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.MenuThemeChoice = CBOX_MenuMusicTheme.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.LanguageChoice = CBOX_LanguageOptions.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.MainMenuChoice = CBOX_MenuOptions.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.DisplaySteamProfileEnabled = CHK_DisplaySteamProfile.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.DoNotTrackEnabled = CHK_DoNotTrack.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.DisplayExactHPValueEnabled = CHK_DisplayExactHPValue.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.FastIntroFadeEnabled = CHK_FastIntroFade.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.HeadBobEnabled = CHK_HeadBob.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.ToggleSprintEnabled = CHK_ToggleSprint.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.DisplayExactHPValueEnabled = CHK_DisplayExactHPValue.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.RichPresenceEnabled = CHK_RichPresence.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.PublicLobbyEnabled = CHK_PublicLobby.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.HideIPEnabled = CHK_HideIP.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.ToggleSearchEnabled = CHK_ToggleSearch.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.SCP079ToggleViewEnabled = CHK_SCP079ToggleView.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.SCP079EnablePostProcessingEnabled = CHK_SCP079EnablePostProcessing.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.BrightnessValue = double.Parse(TBOX_Brightness.Text);
            SCPSLSettingChanger.Properties.Settings.Default.SensitivityValue = double.Parse(TBOX_Sensitivty.Text);
            SCPSLSettingChanger.Properties.Settings.Default.InvertYAxisEnabled = CHK_InvertYAxisChoice.Checked;
            SCPSLSettingChanger.Properties.Settings.Default.E11SightChoice = CBOX_E11Sight.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.E11BarrelChoice = CBOX_E11Barrel.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.E11OtherChoice = CBOX_E11Other.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.Project90SightChoice = CBOX_Project90Sight.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.Project90BarrelChoice = CBOX_Project90Barrel.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.Project90OtherChoice = CBOX_Project90Other.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.MP7SightChoice = CBOX_MP7Sight.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.MP7BarrelChoice = CBOX_MP7Barrel.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.MP7OtherChoice = CBOX_MP7Other.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.USPSightChoice = CBOX_USPSight.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.USPBarrelChoice = CBOX_USPBarrel.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.USPOtherChoice = CBOX_MP7Other.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.COM15SightChoice = CBOX_COM15Sight.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.COM15BarrelChoice = CBOX_COM15Barrel.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.COM15OtherChoice = CBOX_COM15Other.SelectedIndex;
            SCPSLSettingChanger.Properties.Settings.Default.Save();
        }

        private void LoadValues()
        {
            CBOX_ScreenResolution.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.ScreenResolutionChoice;
            CBOX_WindowMode.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.WindowModeChoice;
            CHK_VSync.Checked = SCPSLSettingChanger.Properties.Settings.Default.VSyncEnabled;
            CBOX_FPSLimit.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.FPSLimitChoice;
            CBOX_TextureQuality.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.TextureQualityChoice;
            CBOX_MaxBlood.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.MaxBloodAmountChoice;
            CHK_Shadows.Checked = SCPSLSettingChanger.Properties.Settings.Default.ShadowsEnabled;
            CBOX_ShadowResolution.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.ShadowResolutionChoice;
            CBOX_ShadowDistance.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.ShadowDistanceChoice;
            CHK_MotionBlur.Checked = SCPSLSettingChanger.Properties.Settings.Default.MotionBlurEnabled;
            CHK_ColorCorrection.Checked = SCPSLSettingChanger.Properties.Settings.Default.ColorCorrectionEnabled;
            CHK_AntiAliasing.Checked = SCPSLSettingChanger.Properties.Settings.Default.AntiAliasingEnabled;
            CHK_LightRenderingMode.Checked = SCPSLSettingChanger.Properties.Settings.Default.LightRenderingModeEnabled;
            TBOX_MasterVolumeValue.Text = SCPSLSettingChanger.Properties.Settings.Default.MasterVolumeValue.ToString();
            TBOX_SoundEffectsVolume.Text = SCPSLSettingChanger.Properties.Settings.Default.SoundEffectsVolumeValue.ToString();
            TBOX_VoiceChatValue.Text = SCPSLSettingChanger.Properties.Settings.Default.VoiceChatVolumeValue.ToString();
            TBOX_MenuMusicValue.Text = SCPSLSettingChanger.Properties.Settings.Default.MenuMusicVolumeValue.ToString();
            TBOX_InterfaceVolumeValue.Text = SCPSLSettingChanger.Properties.Settings.Default.InterfaceVolumeValue.ToString();
            CHK_SameMenuAndInterface.Checked = SCPSLSettingChanger.Properties.Settings.Default.SameMenuMusicAndInterfaceVolumeValueEnabled;
            CBOX_MenuMusicTheme.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.MenuThemeChoice;
            CBOX_LanguageOptions.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.LanguageChoice;
            CBOX_MenuOptions.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.MainMenuChoice;
            CHK_DisplaySteamProfile.Checked = SCPSLSettingChanger.Properties.Settings.Default.DisplaySteamProfileEnabled;
            CHK_DoNotTrack.Checked = SCPSLSettingChanger.Properties.Settings.Default.DoNotTrackEnabled;
            CHK_DisplayExactHPValue.Checked = SCPSLSettingChanger.Properties.Settings.Default.DisplayExactHPValueEnabled;
            CHK_FastIntroFade.Checked = SCPSLSettingChanger.Properties.Settings.Default.FastIntroFadeEnabled;
            CHK_HeadBob.Checked = SCPSLSettingChanger.Properties.Settings.Default.HeadBobEnabled;
            CHK_ToggleSprint.Checked = SCPSLSettingChanger.Properties.Settings.Default.ToggleSprintEnabled;
            CHK_DisplayExactHPValue.Checked = SCPSLSettingChanger.Properties.Settings.Default.DisplayExactHPValueEnabled;
            CHK_RichPresence.Checked = SCPSLSettingChanger.Properties.Settings.Default.RichPresenceEnabled;
            CHK_PublicLobby.Checked = SCPSLSettingChanger.Properties.Settings.Default.PublicLobbyEnabled;
            CHK_HideIP.Checked = SCPSLSettingChanger.Properties.Settings.Default.HideIPEnabled;
            CHK_ToggleSearch.Checked = SCPSLSettingChanger.Properties.Settings.Default.ToggleSearchEnabled;
            CHK_SCP079ToggleView.Checked = SCPSLSettingChanger.Properties.Settings.Default.SCP079ToggleViewEnabled;
            CHK_SCP079EnablePostProcessing.Checked = SCPSLSettingChanger.Properties.Settings.Default.SCP079EnablePostProcessingEnabled;
            TBOX_Brightness.Text = SCPSLSettingChanger.Properties.Settings.Default.BrightnessValue.ToString();
            TBOX_Sensitivty.Text = SCPSLSettingChanger.Properties.Settings.Default.SensitivityValue.ToString();
            CHK_InvertYAxisChoice.Checked = SCPSLSettingChanger.Properties.Settings.Default.InvertYAxisEnabled;
            CBOX_E11Sight.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.E11SightChoice;
            CBOX_E11Barrel.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.E11BarrelChoice;
            CBOX_E11Other.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.E11OtherChoice;
            CBOX_Project90Sight.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.Project90SightChoice;
            CBOX_Project90Barrel.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.Project90BarrelChoice;
            CBOX_Project90Other.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.Project90OtherChoice;
            CBOX_MP7Sight.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.MP7SightChoice;
            CBOX_MP7Barrel.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.MP7BarrelChoice;
            CBOX_MP7Other.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.MP7OtherChoice;
            CBOX_USPSight.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.USPSightChoice;
            CBOX_USPBarrel.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.USPBarrelChoice;
            CBOX_USPOther.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.USPOtherChoice;
            CBOX_COM15Sight.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.COM15SightChoice;
            CBOX_COM15Barrel.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.COM15BarrelChoice;
            CBOX_COM15Other.SelectedIndex = SCPSLSettingChanger.Properties.Settings.Default.COM15OtherChoice;
        }
    }
}
