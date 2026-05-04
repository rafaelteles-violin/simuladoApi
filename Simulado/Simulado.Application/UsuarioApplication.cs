using AutoMapper;
using Simulado.Application.Interface;
using Simulado.Application.Status;
using Simulado.Application.ViewModel;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioApplication(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<ServerStatus> Adicionar(UsuarioViewModel usuarioVm)
        {
            var usuario = _mapper.Map<Usuario>(usuarioVm);

            var idsIgreja = usuarioVm.Igrejas.Select(x => x.IgrejaId).ToList();

            usuario.AdicionarIgrejasAoUsuario(idsIgreja);

            if (!usuario.ValidarEntidade().IsValid)
            {
                return await Task.FromResult(new ServerStatus(usuario.ValidarEntidade()
                                 .Errors.Select(x => x.ErrorMessage).ToList()));
            }

            await _usuarioRepository.Adicionar(usuario);

            if (!await _usuarioRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao adicionar usuario");

            return await Task.FromResult(new ServerStatus("Cadastro realizado com sucesso!"));
        }

        public async Task<ServerStatus> Atualizar(UsuarioViewModel usuarioVm)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioVm.UsuarioId);

            if (!usuario.ValidarEntidade().IsValid)
            {
                return await Task.FromResult(new ServerStatus(usuario.ValidarEntidade()
                                 .Errors.Select(x => x.ErrorMessage).ToList()));
            }

            _usuarioRepository.Atualizar(usuario);

            if (!await _usuarioRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao atualizar usuario");

            return await Task.FromResult(new ServerStatus("Cadastro atualizado com sucesso!"));
        }

        public async Task<ServerStatus> AtualizarEmail(UsuarioViewModel usuarioVm)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioVm.UsuarioId);

            if (!usuario.ValidarEntidade().IsValid)
            {
                return await Task.FromResult(new ServerStatus(usuario.ValidarEntidade()
                                 .Errors.Select(x => x.ErrorMessage).ToList()));
            }

            _usuarioRepository.Atualizar(usuario);

            if (!await _usuarioRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao atualizar email");

            return await Task.FromResult(new ServerStatus("Email atualizado com sucesso!"));
        }

        public async Task<ServerStatus> AtualizarSenha(UsuarioViewModel usuarioVm)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioVm.UsuarioId);

            if (!usuario.ValidarEntidade().IsValid)
            {
                return await Task.FromResult(new ServerStatus(usuario.ValidarEntidade()
                                 .Errors.Select(x => x.ErrorMessage).ToList()));
            }

            _usuarioRepository.Atualizar(usuario);

            if (!await _usuarioRepository.UnitOfWorks.Commit())
                throw new Exception("Erro interno ao atualizar email");

            return await Task.FromResult(new ServerStatus("Email atualizado com sucesso!"));
        }
         
        public async Task<List<IgrejaViewModel>> ObterIgrejasDoUsuario(Guid usuarioId)
        {
            var usuarioIgrejas = await _usuarioRepository.ObterIgrejasDoUsuario(usuarioId);
            return usuarioIgrejas.Select(IgrejaViewModel.Mapear).ToList();
        }

        public async Task<List<UsuarioViewModel>> ObterTodos()
        {
            var usuarios = await _usuarioRepository.ObterTodosUsuarios();
            return usuarios.Select(UsuarioViewModel.Mapear).ToList();
        }
    }
}
