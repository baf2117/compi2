var express = require('express');
var router = express.Router();

router.get('/', function(req, res, next) {
    var user = req.query.user;
    var pass = req.query.pass;
    console.log(user);
    var edge = require('edge');
    var ingreso = "SELECT * FROM usuarios where usuario = \'"+user+"\' and pass=\'"+pass+"\';";
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
            if(result.length>0){

                res.render('log1');

            }else{
                res.render('log2');
            }
        }
        else {

            res.render('log2');
        }
    });

});

module.exports = router;