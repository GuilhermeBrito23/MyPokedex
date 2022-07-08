using QuickType;
using System;
using System.Net.Http;
using System.Windows.Forms;
namespace Pokedex
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string apiUrl ="";
            if (String.IsNullOrEmpty(txtNome.Text)!=true && String.IsNullOrEmpty(txtNum.Text)!= true)
            {
                MessageBox.Show("Por gentileza preencher apenas um campo");
                return;
            }
            else if (String.IsNullOrEmpty(txtNome.Text) != true && String.IsNullOrEmpty(txtNum.Text) == true)
            {

                 apiUrl = $"https://pokeapi.co/api/v2/pokemon/{txtNome.Text.ToLower()}";

            }else if (String.IsNullOrEmpty(txtNome.Text) == true && String.IsNullOrEmpty(txtNum.Text) != true)
            {
                 apiUrl = $"https://pokeapi.co/api/v2/pokemon/{txtNum.Text.ToLower()}";
            }
            else
            {
                MessageBox.Show("Campos Vazio!");
                return;
            }


            using (HttpClient client = new HttpClient())
            {
                var apiResponse = client.GetAsync(apiUrl).Result;
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = apiResponse.Content.ReadAsStringAsync().Result;


                    var pokemon = Pokemon.FromJson(result);
                    lblNum.Text = pokemon.Id.ToString();
                    lblNome.Text = pokemon.Name;
                    string types = "";
                    for (int i = 0; i < pokemon.Types.Length; i++)
                    {
                        if (pokemon.Types.Length > 1)
                        {
                            types += "/" + pokemon.Types[i].Type.Name.ToString();
                        }
                        else
                        {
                            types += pokemon.Types[i].Type.Name.ToString();
                        }

                    }
                    lblTipo.Text = types;

                    picBoxPokemon.ImageLocation = pokemon.Sprites.FrontDefault.AbsoluteUri;
                }
            }


        }
    }
}
