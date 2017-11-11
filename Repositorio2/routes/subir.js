var express = require('express');
var router = express.Router();

router.get('/', function(req, res, next) {
    res.render('index');
});
router.post('/', function(req, res, next) {
    var user = req.body.user;
    var cont = req.body.cont;
    var nombre = req.body.nombre;
    var desc = req.body.desc;
    var edge = require('edge');
    var ingreso = "insert into clases(nombre,usuario,fecha,url,descripcion,contenido) values (\'"+nombre+"\',\'"+user+"\',SYSDATETIME(),\'http//:localhost:300/mynube/"+nombre+"\',\'"+desc+"\',\'"+cont+"\');";
    console.log(ingreso);
    var params = {
        connectionString: "Server=BAF21\\BAF21;Database=Repositorio;Integrated Security=True",
        source: ingreso

    };
    var getData = edge.func( 'sql', params);
    getData(null, function (error, result) {
        if (error) {

            res.render('log2');
        }
        if (result) {
           res.render('log1')
        }
        else {

            res.render('log2');
        }
    });

});

module.exports = router;