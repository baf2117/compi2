var express = require('express');
var router = express.Router();


/* GET home page. */
router.get('/', function(req, res, next) {

    res.render('registro');
});



router.post('/',function(req,res,next){
    var user = req.body.user;
    var pass = req.body.pass;
    var nombre = req.body.nombre;

    var edge = require('edge');
    var ingreso = "SELECT * FROM usuarios where usuario = \'"+user+"\';";
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

                    return res.redirect('/registro');
            }else{
                var url = "/registro2?user="+user+"&pass="+pass+"&nombre="+nombre;
                return  res.redirect(url);
            }
        }
        else {
            console.log("No results");
            return res.redirect('/sesion');
        }
    });

});

module.exports = router;