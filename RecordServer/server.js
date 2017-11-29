var express = require('express');
var fs = require('fs');
var path = require('path');
var app = express();


app.listen(8000,function(){
	console.log('Listening!');
});

app.use(function(res,res,next){
	res.header("Access-Control-Allow-Origin","*");
	res.header("Access-Control-Allow-Headers","Origin, X-Requested-With, Content-Type, Accept");
	next();

});

app.get('/', function (req,res){
	console.log('Request');
	res.sendFile('scores.json', {root:__dirname});
});
app.get('/submit/:name/:score',submitScore);


function submitScore(req,res){
	var name = req.params.name;
	var score = req.params.score;

	fs.readFile(path.join(__dirname,"scores.json"), 'utf8', function(err,data){
		data = JSON.parse(data);
		//console.log(data);
		var scores = data['scores'];
		for (var i in scores){
			var item = scores[i];
			console.log(item['name']);
			console.log(item['score']);
		}
		res.send("OK");
		
	})
}



