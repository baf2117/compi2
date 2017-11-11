var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
    var edge = require('edge');
    var ingreso = "select a.Nombre as nombre, b.usuario as usuario, a.fecha as fecha from clases a, Usuarios b where a.usuario = b.usuario;";
    console.log(ingreso);
    var params = {
        connectionString: "Server=BAF21\\BAF21;Database=Repositorio;Integrated Security=True",
        source: ingreso

    };
    var getData = edge.func( 'sql', params);

    getData(null, function (error, result) {
        if (error) {
            console.log(error); return;
        }
        if (result) {

          console.log((result));
          res.render('inicio',{contenido: result});

        }
        else {
            console.log("No results");
            return res.redirect('/sesion');
        }
    });

});


module.exports = router;