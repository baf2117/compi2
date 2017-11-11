var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
    var user = req.query.user;
    var nombre = req.query.nombre;
    var pass = req.query.pass
    var edge = require('edge');
    var ingreso = "insert into Usuarios (nombre,usuario,pass) values (\'"+nombre+"\',\'"+user+"\',\'"+pass+"\');";
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
            return res.redirect('/clases');

        }
        else {
            console.log("No results");
            return res.redirect('/');
        }
    });

});


module.exports = router;