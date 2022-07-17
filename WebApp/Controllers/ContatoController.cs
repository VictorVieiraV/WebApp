using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("ResultadoSoma")] Validacao validacao)
        {
            if (validacao.ResultadoSoma != 20)
            {
                ModelState.AddModelError(nameof(Validacao.ResultadoSoma), "Valor incorreto!");
            }
            else
            {
                return RedirectToAction("Create");
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Email,Cpf,Senha,Documento")] Contato contato)
        {
            if (contato.Cpf != null && contato.Email != null && contato.Senha != null && contato.Documento != null)
            {
                if (!ValidarCpf(contato.Cpf))
                {
                    ModelState.AddModelError(nameof(Contato.Cpf), "Cpf inválido!");
                    return View();
                }
                if (!ValidarSenha(contato.Senha))
                {
                    ModelState.AddModelError(nameof(Contato.Senha), "Senha inválida!");
                    return View();
                }
                if (!ValidarEmail(contato.Email))
                {
                    ModelState.AddModelError(nameof(Contato.Email), "E-mail inválido!");
                    return View();
                }
                if (!ValidarDocumento(contato.Documento))
                {
                    ModelState.AddModelError(nameof(Contato.Documento), "Formato inválido, inserir no formato .pdf");
                    return View();
                }
            }
            return View();
        }

        public bool ValidarDocumento(IFormFile documento)
        {
            var nomeArquivo = documento.FileName;
            var tipoArquivo = documento.ContentType;

            if (tipoArquivo != "application/pdf")
            {
                return false;
            }

            return true;
        }
        public bool ValidarEmail(string email)
        {
            if (email.StartsWith("@") || email.EndsWith("@") || !email.Contains("@"))
            {
                return false;
            }
            return true;
        }
        public bool ValidarSenha(string senha)
        {
            string[] numeros = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] letrasMaiusculas = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] caracteresEspeciais = { @"!", @"”", @"#", @"$", @"%", @"&", @"’", @"(", @")", @"*", @"+", @",", @"-", @".", @"/", @":", @";", @"<", @"=", @">", @"?", @"@", @"[", @"\", @"]", @"^", @"_", @"`", @"{", @"|", @"}", @"~" };

            int validador = 0;

            foreach (var item in numeros)
            {
                if (senha.Contains(item))
                {
                    validador++;
                    break;
                }
            }
            foreach (var item in letrasMaiusculas)
            {
                if (senha.Contains(item))
                {
                    validador++;
                    break;
                }
            }
            foreach (var item in caracteresEspeciais)
            {
                if (senha.Contains(item))
                {
                    validador++;
                    break;
                }
            }

            if (senha.Length >= 4 && senha.Length <= 10)
            {
                validador++;
            }

            if (validador != 4)
            {
                return false;
            }

            return true;
        }
        public bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;

            string digito;

            int soma;

            int resto;

            cpf = cpf.Trim();

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)

                return false;

            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}