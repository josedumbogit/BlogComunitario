using BlogComunitario.Models;
using BlogComunitario.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogComunitario.Controllers
{
    public class AccountController : Controller
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(BlogDbContext blogDbContext, IEmailSender emailSender, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _blogDbContext = blogDbContext;
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Página principal
        public IActionResult Index()
        {         
            return View();
        }

        // Registro de usuário
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                // Criar o usuário no banco de dados
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Gerar o token de confirmação de e-mail
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Gerar o link de confirmação de e-mail
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                    // Enviar o e-mail de confirmação
                    await _emailSender.SendEmailAsync(model.Email, "Confirme seu e-mail",
                        $"Por favor, confirme sua conta clicando neste link: <a href='{confirmationLink}'>link</a>");

                    // Redirecionar para uma página informando que o e-mail de confirmação foi enviado
                    return RedirectToAction("RegisterConfirmation");
                }

                // Caso haja erros na criação do usuário, adicionar ao ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

       //Página de Erro
        public IActionResult Error()
        {
            return View();
        }
        
        //Confirmação de e-mail
        public IActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpGet]
        [Route("Account/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"O usuário com o ID '{userId}' não foi encontrado.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }

            return View("Error");
        }

        // Login de usuário
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Buscar o usuário pelo e-mail
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // Verificar se o e-mail foi confirmado
                    if (!user.EmailConfirmed)
                    {
                        ModelState.AddModelError(string.Empty, "Você precisa confirmar seu e-mail para fazer login.");
                        return View(model);
                    }

                    // Verificar se o usuário está bloqueado
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Sua conta está temporariamente bloqueada devido a várias tentativas de login falhas. Tente novamente mais tarde.");
                        return View(model);
                    }

                    // Tentar realizar o login
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        // Resetar contador de falhas após login bem-sucedido
                        await _userManager.ResetAccessFailedCountAsync(user);
                        return RedirectToAction("Index", "Home");
                    }

                    if (result.IsLockedOut)
                    {
                        // Se o usuário for bloqueado após tentativas falhas, exibir mensagem de bloqueio
                        ModelState.AddModelError(string.Empty, "Sua conta foi bloqueada temporariamente devido a várias tentativas de login falhas.");
                        return View(model);
                    }
                    else
                    {
                        // Aumentar o contador de falhas
                        await _userManager.AccessFailedAsync(user);

                        ModelState.AddModelError(string.Empty, "Login inválido. Verifique suas credenciais.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário não encontrado.");
                }
            }

            return View(model);
        }

        // Confirmação do registo
        [HttpGet]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }

        // Método para efectuar Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Efetuar o logout do usuário
            await _signInManager.SignOutAsync();

            // Redirecionar para a página inicial ou qualquer outra página
            return RedirectToAction("Login", "Account");
        }

    }
}
