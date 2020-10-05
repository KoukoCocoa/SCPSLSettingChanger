using System;
using System.Globalization;
using System.IO;
using System.Linq;
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
        }

        private void BTN_GenerateConfig_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SCP Secret Laboratory")))
            {
                PlayerPrefsSl.Set("gammavalue", (float)NUD_Brightness.Value * 0.02f);
                if (((float)NUD_Sensitivty.Value * 0.03f) >= 0.1f)
                    PlayerPrefsSl.Set("Sens", (float)NUD_Sensitivty.Value * 0.03f);
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
                if (CHK_Shadows.Checked)
                {
                    PlayerPrefsSl.Set("gfxsets_shadows", 1);
                    PlayerPrefsSl.Set("gfxsets_shadows", CHK_Shadows.Checked);
                    PlayerPrefsSl.Set("gfxsets_shadres", CBOX_ShadowResolution.SelectedIndex);
                    PlayerPrefsSl.Set("gfxsets_shaddis_new", CBOX_ShadowDistance.SelectedIndex);
                }
                else
                {
                    PlayerPrefsSl.Set("gfxsets_shadows", 0);
                    PlayerPrefsSl.Set("gfxsets_shadows", CHK_Shadows.Checked);
                }
                PlayerPrefsSl.Set("gfxsets_mb", CHK_MotionBlur.Checked ? 1 : 0);
                PlayerPrefsSl.Set("gfxsets_mb", CHK_MotionBlur.Checked);
                PlayerPrefsSl.Set("gfxsets_cc", CHK_ColorCorrection.Checked ? 1 : 0);
                PlayerPrefsSl.Set("gfxsets_cc", CHK_ColorCorrection.Checked);
                PlayerPrefsSl.Set("gfxsets_aa", CHK_AntiAliasing.Checked ? 1 : 0);
                PlayerPrefsSl.Set("gfxsets_aa", CHK_AntiAliasing.Checked);
                PlayerPrefsSl.Set("gfxsets_hp", CHK_LightRenderingMode.Checked ? 0 : 1);
                PlayerPrefsSl.Set("gfxsets_hp", CHK_LightRenderingMode.Checked);
                PlayerPrefsSl.Set("AudioSettings_Master", (float)NUD_MasterVolume.Value * 0.01f);
                PlayerPrefsSl.Set("AudioSettings_Effects", (float)NUD_SoundEffects.Value * 0.01f);
                PlayerPrefsSl.Set("AudioSettings_VoiceChat", (float)NUD_VoiceChat.Value * 0.01f);
                PlayerPrefsSl.Set("AudioSettings_MenuMusic", (float)NUD_MenuMusic.Value * 0.01f);
                PlayerPrefsSl.Set("AudioSettings_Interface", (float)NUD_InterfaceVolume.Value * 0.01f);
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
                PlayerPrefsSl.Set("ModeSwitchSetting", CHK_SCP079ToggleView.Checked);
                PlayerPrefsSl.Set("PostProcessing", CHK_SCP079EnablePostProcessing.Checked);
                PlayerPrefsSl.Set("translation_path", LanguageValues[CBOX_LanguageOptions.SelectedIndex]);
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
                PlayerPrefsSl.Set("graphics_api", CBOX_GraphicsAPI.SelectedIndex);
                PlayerPrefsSl.Refresh();
                MessageBox.Show("SCP:SL game settings have been changed now!", "Successfully written new file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("The game directory where the settings belong to is not there (Do you have the game installed?)", "Directory does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            FormatInput(true, 0);
        }

        private void NUD_MenuMusic_ValueChanged(object sender, EventArgs e)
        {
            if (CHK_SameMenuAndInterface.Checked)
                FormatInput(false, 1);
        }

        private void NUD_InterfaceVolume_ValueChanged(object sender, EventArgs e)
        {
            if (CHK_SameMenuAndInterface.Checked)
                FormatInput(false, 2);
        }

        private void FormatInput(bool UsedCheckbox, int choice)
        {
            if (UsedCheckbox)
            {
                if (NUD_MenuMusic.Value < NUD_InterfaceVolume.Value)
                    NUD_MenuMusic.Value = NUD_InterfaceVolume.Value;
                else
                    NUD_InterfaceVolume.Value = NUD_MenuMusic.Value;
            }
            else
            {
                switch (choice)
                {
                    case 1:
                        NUD_InterfaceVolume.Value = NUD_MenuMusic.Value;
                        break;
                    case 2:
                        NUD_MenuMusic.Value = NUD_InterfaceVolume.Value;
                        break;
                }
            }
        }

        private void LoadValues()
        {
            CultureInfo OS = CultureInfo.CurrentUICulture;
            string OSName = OS.DisplayName.Substring(0, OS.DisplayName.IndexOf(' '));
            bool FoundOSInList = false;
            for (int i = 0; i < CBOX_LanguageOptions.Items.Count; i++)
                if (CBOX_LanguageOptions.Items[i].ToString().Contains(OSName))
                {
                    CBOX_LanguageOptions.SelectedIndex = i;
                    FoundOSInList = true;
                    break;
                }
            if (!FoundOSInList)
                CBOX_LanguageOptions.SelectedIndex = 5;
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
            int FrameRate = PlayerPrefsSl.Get("MaxFramerate", 0);
            for (int i = 0; i < Framerates.Length; i++)
            {
                if (Framerates[i] == FrameRate)
                {
                    CBOX_FPSLimit.SelectedIndex = i;
                    break;
                }
            }
            CBOX_ScreenResolution.SelectedIndex = PlayerPrefsSl.Get("SavedResolutionSet", 0);
            CBOX_WindowMode.SelectedIndex = PlayerPrefsSl.Get("ScreenMode", 0);
            CHK_VSync.Checked = PlayerPrefsSl.Get("gfxsets_vsync", false);
            CBOX_TextureQuality.SelectedIndex = PlayerPrefsSl.Get("gfxsets_textures", 0);
            CBOX_MaxBlood.SelectedIndex = PlayerPrefsSl.Get("gfxsets_maxblood   ", 0);
            CHK_Shadows.Checked = PlayerPrefsSl.Get("gfxsets_shadows", false);
            CBOX_ShadowResolution.SelectedIndex = PlayerPrefsSl.Get("gfxsets_shadres", 0);
            CBOX_ShadowDistance.SelectedIndex = PlayerPrefsSl.Get("gfxsets_shaddis_new", 0);
            CHK_MotionBlur.Checked = PlayerPrefsSl.Get("gfxsets_mb", false);
            CHK_ColorCorrection.Checked = PlayerPrefsSl.Get("gfxsets_cc", false);
            CHK_AntiAliasing.Checked = PlayerPrefsSl.Get("gfxsets_aa", false);
            CHK_LightRenderingMode.Checked = PlayerPrefsSl.Get("gfxsets_hp", false);
            NUD_MasterVolume.Value = (decimal)PlayerPrefsSl.Get("AudioSettings_Master", 0f);
            NUD_SoundEffects.Value = (decimal)PlayerPrefsSl.Get("AudioSettings_Effects", 0f);
            NUD_VoiceChat.Value = (decimal)PlayerPrefsSl.Get("AudioSettings_VoiceChat", 0f);
            NUD_MenuMusic.Value = (decimal)PlayerPrefsSl.Get("AudioSettings_MenuMusic", 0f);
            NUD_InterfaceVolume.Value = (decimal)PlayerPrefsSl.Get("AudioSettings_Interface", 0f);
            CHK_SameMenuAndInterface.Checked = PlayerPrefsSl.Get("MaintainSliderProportions", false);
            CBOX_MenuMusicTheme.SelectedIndex = PlayerPrefsSl.Get("MenuTheme", 0);
            CHK_DisplaySteamProfile.Checked = PlayerPrefsSl.Get("DisplaySteamProfile", false);
            CHK_DoNotTrack.Checked = PlayerPrefsSl.Get("DNT", false);
            CHK_DisplayExactHPValue.Checked = PlayerPrefsSl.Get("HealthBarShowsExact", false);
            CHK_FastIntroFade.Checked = PlayerPrefsSl.Get("ClassIntroFastFade", false);
            CHK_HeadBob.Checked = PlayerPrefsSl.Get("HeadBob", false);
            CHK_ToggleSprint.Checked = PlayerPrefsSl.Get("ToggleSprint", false);
            CHK_RichPresence.Checked = PlayerPrefsSl.Get("RichPresence", false);
            CHK_PublicLobby.Checked = PlayerPrefsSl.Get("PublicLobby", false);
            CHK_HideIP.Checked = PlayerPrefsSl.Get("HideIP", false);
            CHK_ToggleSearch.Checked = PlayerPrefsSl.Get("ToggleSearch", false);
            CHK_SCP079ToggleView.Checked = PlayerPrefsSl.Get("ModeSwitchSetting079", false);
            CHK_SCP079EnablePostProcessing.Checked = PlayerPrefsSl.Get("PostProcessing079", false);
            NUD_Brightness.Value = (decimal)PlayerPrefsSl.Get("gammavalue", 0f);
            NUD_Sensitivty.Value = (decimal)PlayerPrefsSl.Get("Sens", 0f);
            CHK_InvertYAxisChoice.Checked = PlayerPrefsSl.Get("y_invert", false);
            CBOX_E11Sight.SelectedIndex = PlayerPrefsSl.Get("W_2_0", 0);
            CBOX_E11Barrel.SelectedIndex = PlayerPrefsSl.Get("W_2_1", 0);
            CBOX_E11Other.SelectedIndex = PlayerPrefsSl.Get("W_2_2", 0);
            CBOX_Project90Sight.SelectedIndex = PlayerPrefsSl.Get("W_1_0", 0);
            CBOX_Project90Barrel.SelectedIndex = PlayerPrefsSl.Get("W_1_1", 0);
            CBOX_Project90Other.SelectedIndex = PlayerPrefsSl.Get("W_1_2", 0);
            CBOX_MP7Sight.SelectedIndex = PlayerPrefsSl.Get("W_3_0", 0);
            CBOX_MP7Barrel.SelectedIndex = PlayerPrefsSl.Get("W_3_1", 0);
            CBOX_MP7Other.SelectedIndex = PlayerPrefsSl.Get("W_3_2", 0);
            CBOX_USPSight.SelectedIndex = PlayerPrefsSl.Get("W_5_0", 0);
            CBOX_USPBarrel.SelectedIndex = PlayerPrefsSl.Get("W_5_1", 0);
            CBOX_USPOther.SelectedIndex = PlayerPrefsSl.Get("W_5_2", 0);
            CBOX_COM15Sight.SelectedIndex = PlayerPrefsSl.Get("W_0_0", 0);
            CBOX_COM15Barrel.SelectedIndex = PlayerPrefsSl.Get("W_0_1", 0);
            CBOX_COM15Other.SelectedIndex = PlayerPrefsSl.Get("W_0_2", 0);
            CBOX_GraphicsAPI.SelectedIndex = PlayerPrefsSl.Get("graphics_api", 0);
        }
    }
}
