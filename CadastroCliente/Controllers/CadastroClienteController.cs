using Microsoft.AspNetCore.Mvc;
using CadastroClienteService.Model;
using CadastroClienteServices.Service;

namespace CadastroCliente.Controllers
{
    public class CadastroClienteController : Controller
    {
        private readonly ICadastroServices _cadastroServices;

        public CadastroClienteController(ICadastroServices cadastroServices)
        {
            _cadastroServices = cadastroServices;
        }

        public async ValueTask<IActionResult> Index()
        {
            var retorno = await _cadastroServices.ListarCliente();

            if (retorno == null)
            {
                return NotFound();
            }

            else
            {
                return View(retorno);
            }
        }

        [HttpGet]
        public async ValueTask<IActionResult> BuscarEndereco(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var retorno = await _cadastroServices.BuscarEnderecoCliente(id);

            if (retorno == null)
            {
                return NotFound();
            }

            else
            {
                return View(retorno);
            }
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> Cadastrar(ClienteEnderecoViewModel cliente)
        {
            if (ModelState.IsValid && cliente.Idade > 0)
            {
                await _cadastroServices.CadastrarCliente(cliente);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async ValueTask<IActionResult> Editar(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var cliente = await _cadastroServices.BuscarCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        public async ValueTask<IActionResult> Editar(ClienteEnderecoViewModel cliente)
        {
            if (ModelState.IsValid && cliente.Idade > 0)
            {
                await _cadastroServices.EditarCliente(cliente);

                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        [HttpPost]
        public async ValueTask<IActionResult> Excluir(int id)
        {
            if (ModelState.IsValid)
            {
                await _cadastroServices.ExcluirCliente(id);

                return RedirectToAction("Index");
            }

            return View(id);
        }
    }
}
