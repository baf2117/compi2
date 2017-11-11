var express = require('express');
var path = require('path');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

var index = require('./routes/index');
var sesion = require('./routes/sesion');
var inicio = require('./routes/inicio');
var clases = require('./routes/clases');
var vista = require('./routes/vista');
var registro = require('./routes/registro');
var registro2 = require('./routes/registro2');
var ingresoc = require('./routes/ingresoc');
var subir = require('./routes/subir');
var app = express();

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');

// uncomment after placing your favicon in /public
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', index);
app.use('/sesion', sesion);
app.use('/clases', clases);
app.use('/inicio', inicio);
app.use('/vista', vista);
app.use('/registro', registro);
app.use('/registro2', registro2);
app.use('/ingresoc', ingresoc);
app.use('/subir', subir);
// catch 404 and forward to error handler
app.use(function(req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

// error handler
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.render('error');
});

module.exports = app;
