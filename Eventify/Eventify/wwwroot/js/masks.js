window.aplicarMascaras = () => {
    Inputmask({ "mask": "999.999.999-99" }).mask(document.getElementById("cpf"));
    Inputmask({ "mask": "(99) 99999-9999" }).mask(document.getElementById("celular"));
    Inputmask({ "mask": "99999-999" }).mask(document.getElementById("cep"));
}

window.ativarMascarasPagamento = () => {
    const elNumero = document.getElementById("cartao-numero");
    if (elNumero) {
        Inputmask("9999 9999 9999 9999").mask(elNumero);
    }

    const elValidade = document.getElementById("cartao-validade");
    if (elValidade) {
        Inputmask("99/99").mask(elValidade);
    }

    const elCvv = document.getElementById("cartao-cvv");
    if (elCvv) {
        Inputmask("999").mask(elCvv);
    }
};