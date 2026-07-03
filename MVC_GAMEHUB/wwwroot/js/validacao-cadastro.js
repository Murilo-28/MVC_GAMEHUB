document.querySelector('form').addEventListener('submit', function (e) {
    let valido = true;

    document.querySelectorAll('.erro-js').forEach(function (el) { el.remove(); });

    const nome = document.querySelector('input[name="Nome"]');
    const telefone = document.querySelector('input[name="Telefone"]');
    const cpf = document.querySelector('input[name="Cpf"]');
    const email = document.querySelector('input[name="Email"]');
    const senha = document.querySelector('input[name="Senha"]');

    if (nome.value.trim() === '') {
        mostrarErro(nome, 'O nome é obrigatório.');
        valido = false;
    } else if (nome.value.trim().length < 3) {
        mostrarErro(nome, 'O nome deve ter pelo menos 3 caracteres.');
        valido = false;
    }

    if (telefone.value.trim() === '') {
        mostrarErro(telefone, 'O telefone é obrigatório.');
        valido = false;
    } else if (telefone.value.replace(/\D/g, '').length < 10) {
        mostrarErro(telefone, 'Digite um telefone válido com DDD.');
        valido = false;
    }

    if (cpf.value.trim() === '') {
        mostrarErro(cpf, 'O CPF é obrigatório.');
        valido = false;
    } else if (cpf.value.replace(/\D/g, '').length !== 11) {
        mostrarErro(cpf, 'O CPF deve ter 11 dígitos.');
        valido = false;
    }

    if (email.value.trim() === '') {
        mostrarErro(email, 'O email é obrigatório.');
        valido = false;
    } else if (email.value.indexOf('@') < 1 || email.value.lastIndexOf('.') < email.value.indexOf('@')) {
        mostrarErro(email, 'Digite um email válido.');
        valido = false;
    }

    if (senha.value.trim() === '') {
        mostrarErro(senha, 'A senha é obrigatória.');
        valido = false;
    } else if (senha.value.length < 6) {
        mostrarErro(senha, 'A senha deve ter pelo menos 6 caracteres.');
        valido = false;
    }

    if (!valido) { e.preventDefault(); }
});

function mostrarErro(campo, mensagem) {
    campo.style.borderColor = '#ff6b6b';
    const erro = document.createElement('div');
    erro.className = 'erro-js';
    erro.style.cssText = 'color:#ff6b6b; font-size:11px; margin-top:4px;';
    erro.textContent = mensagem;
    campo.parentNode.appendChild(erro);
    campo.addEventListener('input', function () {
        campo.style.borderColor = '#4a2d7a';
        erro.remove();
    }, { once: true });
}