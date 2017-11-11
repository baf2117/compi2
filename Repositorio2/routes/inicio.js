var express = require('express');
var router = express.Router();


/* GET home page. */
router.get('/', function(req, res, next) {

    res.render('inicio', { datos: null});
});



router.post('/',function(req,res,next){
    var user = req.body.nombre;
    var pass = req.body.pass;

    var edge = require('edge');
    var ingreso = "SELECT * FROM usuarios where usuario = \'"+user+"\' and pass=\'"+pass+"\';";
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
            if(result.length>0){

               return  res.redirect('/clases');

            }else{
                return res.redirect('/sesion');
            }
        }
        else {
            console.log("No results");
            return res.redirect('/sesion');
        }
    });

});

module.exports = router;