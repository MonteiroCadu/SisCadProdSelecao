// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function setDelete(id) {

    idParaDeletar = id;

};

function deletaProduto(uri) {    
    window.location.replace(uri + idParaDeletar);
}
