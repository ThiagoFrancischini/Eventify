window.aplicarMascaras = () => {
    Inputmask({ "mask": "999.999.999-99" }).mask(document.getElementById("cpf"));
    Inputmask({ "mask": "(99) 99999-9999" }).mask(document.getElementById("celular"));
    Inputmask({ "mask": "99999-999" }).mask(document.getElementById("cep"));
}