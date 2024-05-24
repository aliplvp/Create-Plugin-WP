using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Create_Plugin
{
    public partial class Creator : Form
    {
        public Creator()
        {
            InitializeComponent();
        }

        void sct(string dir, string file, string data)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }


            if (file != null && data != null)
            {
                FileStream fileStream = new FileStream(dir + @"\" + file, FileMode.Create);
                try
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    fileStream.Write(bytes, 0, bytes.Length);
                }
                finally
                {
                    fileStream.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_plugin_Name.Text))
            {
                MessageBox.Show("نام افزونه نمیتواند خالی باشد","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txt_plugin_Name.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_URI.Text))
            {
                MessageBox.Show("آدرس افزونه نمیتواند خالی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_URI.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_Version1.Text))
            {
                MessageBox.Show("ورژن افزونه نمیتواند خالی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_Version1.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_Version2.Text))
            {
                MessageBox.Show("ورژن افزونه نمیتواند خالی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_Version2.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_PHPVersion1.Text))
            {
                MessageBox.Show("افزونه نمیتواند خالی باشد PHP ورژن", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_PHPVersion1.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_PHPVersion2.Text))
            {
                MessageBox.Show("افزونه نمیتواند خالی باشد PHP ورژن", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_PHPVersion2.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_Description.Text))
            {
                MessageBox.Show("توضیحات افزونه نمیتواند خالی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_Description.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_Author.Text))
            {
                MessageBox.Show("نام نویسنده افزونه نمیتواند خالی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_Author.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_Author_URI.Text))
            {
                MessageBox.Show("آدرس نویسنده افزونه نمیتواند خالی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_Author_URI.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_Define.Text))
            {
                MessageBox.Show("نام ثابت افزونه نمیتواند خالی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_Define.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_plugin_Folder_Name.Text))
            {
                MessageBox.Show("نام افزونه به انگلیسی نمیتواند خالی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_plugin_Folder_Name.Focus();
            }
            else
            {
                string plugin = @"<?php
/**
 * Plugin Name:       " + txt_plugin_Name.Text + @"
 * Plugin URI:        " + txt_plugin_URI.Text + @"
 * Description:       " + txt_plugin_Description.Text + @"
 * Version:           " + txt_plugin_Version1.Text + "." + txt_plugin_Version2.Text + @"
 * Requires PHP:      " + txt_plugin_PHPVersion1.Text + "." + txt_plugin_PHPVersion2.Text + @"
 * Author:            " + txt_plugin_Author.Text + @"
 * Author URI:        " + txt_plugin_Author_URI.Text + @"
*/

//Don't access to page
if (!function_exists('add_action')) {
 echo 'You dont have access in page!';   
 exit();
}

//plugin path
define('" + txt_plugin_Define.Text.ToUpper() + @"_PATH', plugin_dir_path(__FILE__));

//plugin url
define('" + txt_plugin_Define.Text.ToUpper() + @"_URL', plugin_dir_url(__FILE__));

//plugin css url
define('" + txt_plugin_Define.Text.ToUpper() + @"_CSS_URL', trailingslashit(" + txt_plugin_Define.Text.ToUpper() + @"_URL.'assets/css')); 

//plugin img url
define('" + txt_plugin_Define.Text.ToUpper() + @"_IMG_URL', trailingslashit(" + txt_plugin_Define.Text.ToUpper() + @"_URL.'assets/img'));

//plugin js url
define('" + txt_plugin_Define.Text.ToUpper() + @"_JS_URL', trailingslashit(" + txt_plugin_Define.Text.ToUpper() + @"_URL.'assets/js'));

//plugin include PATH
define('" + txt_plugin_Define.Text.ToUpper() + @"_INCLUDE_PATH', trailingslashit(" + txt_plugin_Define.Text.ToUpper() + @"_PATH.'include'));

//plugin moderator PATH
define('" + txt_plugin_Define.Text.ToUpper() + @"_MODERATOR_PATH', trailingslashit(" + txt_plugin_Define.Text.ToUpper() + @"_PATH.'moderator'));

//plugin template PATH
define('" + txt_plugin_Define.Text.ToUpper() + @"_TEMPLATE_PATH', trailingslashit(" + txt_plugin_Define.Text.ToUpper() + @"_PATH.'template'));


if (is_admin()) {
    require 'admin/menu.php';
    require 'admin/page.php';
}

?>";

                string menu = @"<?php
add_action('admin_menu','" + txt_plugin_Define.Text.ToUpper() + @"_admin_menu_fun');

function " + txt_plugin_Define.Text.ToUpper() + @"_admin_menu_fun() {
    $cap = 'install_plugins';
    $menu = add_menu_page('"+ txt_plugin_Name.Text + @"','" + txt_plugin_Name.Text + @"',$cap,'" + txt_plugin_Define.Text.ToLower() + @"-menu-main','" + txt_plugin_Define.Text.ToUpper() + @"_main_menu_fun','','99');
    $submenu = add_submenu_page('" + txt_plugin_Define.Text.ToLower() + @"-menu-main','زیرمنو','زیرمنو',$cap,'plugin-submenu','" + txt_plugin_Define.Text.ToUpper() + @"_submenu_menu_fun');
}
?>";

                string page = @"<?php
function " + txt_plugin_Define.Text.ToUpper() + @"_main_menu_fun() {
    include " + txt_plugin_Define.Text.ToUpper() + @"_TEMPLATE_PATH.'admin_main_menu_html.php';
}

function " + txt_plugin_Define.Text.ToUpper() + @"_submenu_menu_fun() {
    include " + txt_plugin_Define.Text.ToUpper() + @"_TEMPLATE_PATH.'admin_sub_menu_html.php';
}
?>";

                string main_menu = @"<div class=""wrap"">
<h1>" + txt_plugin_Name.Text + @" - صفحه اصلی</h1>
</div>";
                string sub_menu = @"<div class=""wrap"">
<h1>زیر منو</h1>
</div>";

                sct(txt_plugin_Folder_Name.Text, txt_plugin_Folder_Name.Text+".php", plugin);
                sct(txt_plugin_Folder_Name.Text + @"\admin",null,null);
                sct(txt_plugin_Folder_Name.Text + @"\assets", null,null);
                sct(txt_plugin_Folder_Name.Text + @"\assets\img", null,null);
                sct(txt_plugin_Folder_Name.Text + @"\assets\css", null,null);
                sct(txt_plugin_Folder_Name.Text + @"\assets\js", null,null);
                sct(txt_plugin_Folder_Name.Text + @"\include", null,null);
                sct(txt_plugin_Folder_Name.Text + @"\template", null,null);

                sct(txt_plugin_Folder_Name.Text + @"\admin", "menu.php", menu);
                sct(txt_plugin_Folder_Name.Text + @"\admin", "page.php", page);

                sct(txt_plugin_Folder_Name.Text + @"\template", "admin_main_menu_html.php", main_menu);
                sct(txt_plugin_Folder_Name.Text + @"\template", "admin_sub_menu_html.php", sub_menu);

                DialogResult result1 = MessageBox.Show("افزونه تولید شد ، پوشه باز شود؟", "موفقیت", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result1 == DialogResult.Yes)
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = txt_plugin_Folder_Name.Text,
                        FileName = "explorer.exe",
                    };

                    Process.Start(startInfo);
                }
                else
                {
                    DialogResult result = MessageBox.Show("آیا مایلید فایل فشرده تولید شود؟", "موفقیت", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        ZipFile.CreateFromDirectory(txt_plugin_Folder_Name.Text, txt_plugin_Folder_Name.Text + ".zip");
                    }
                    else
                    {
                        Application.Exit();
                    }
                }


                

                


                

            
            }
        }
    }
}
