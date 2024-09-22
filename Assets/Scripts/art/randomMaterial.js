#pragma strict
/*import System.Collections.Generic;
public var  _materials :List.<Material> = new List.<Material>();

function Start (){

    var _material : Material  = _materials[Random.Range(0, _materials.Count)] as Material;
	if(_material)
    gameObject.renderer.material = _material;    

}
*/

var diffColor : Color = Color.white;
var randomRangeDiff : int = 20;
var minDiff : int = 0;
var maxDiff : int = 255;
//var addColor : float = 0;
var HueVariationDiff : int = 10;

private var HueVariationRandom : int = 0;
private var Huecheck : float = 0;

var specColor : Color = Color.white;
var randomRangeSpec : int = 20;
var minSpec : int = 0;
var maxSpec : int = 255;
var HueVariationSpec : int = 10;


function Start (){
	
	var max2 = randomRangeDiff+diffColor.r*255 ;
	if(max2>255)
	max2=255;
	if(max2>maxDiff)
	max2=maxDiff;
	
	var min2 = diffColor.r*255 - randomRangeDiff;
	if(min2<0)
	min2=0;
	if(min2<minDiff)
	min2=minDiff;
		
	var range : float;
	range = Random.Range(min2, max2) ;
	//Debug.Log("range " + range);
	HueVariationRandom = Random.Range(-HueVariationDiff , HueVariationDiff) ;
	//Debug.Log("HueVariationRandom " + HueVariationRandom);
	Huecheck = HueVariationRandom + range;
	//Debug.Log("Huecheck " + Huecheck);
	CheckHue();
	//Debug.Log("Huecheck " + Huecheck);
	var mycolor : Color;
	mycolor.r = Huecheck/255;//+addColor;
	
	HueVariationRandom = Random.Range(-HueVariationDiff , HueVariationDiff) ;
	Huecheck = HueVariationRandom + range;
	CheckHue();
	mycolor.g = Huecheck/255;//+addColor;
	
	HueVariationRandom = Random.Range(-HueVariationDiff , HueVariationDiff) ;
	Huecheck = HueVariationRandom + range;
	CheckHue();
	mycolor.b = Huecheck/255;//+addColor;
	
//	Debug.Log(grey);
	gameObject.renderer.material.SetColor("_Color", mycolor);
	
	//:::::::::::::::::::::::::::::::::::::::::::
	
	max2 = randomRangeSpec+specColor.r*255 ;
	if(max2>255)
	max2=255;
	if(max2>maxSpec)
	max2=maxSpec;
	
	min2 = specColor.r*255 - randomRangeSpec;
	if(min2<0)
	min2=0;
	if(min2<minSpec)
	min2=minSpec;
		
	range = Random.Range(min2, max2) ;
	//Debug.Log("range " + range);
	HueVariationRandom = Random.Range(-HueVariationDiff , HueVariationDiff) ;
	//Debug.Log("HueVariationRandom " + HueVariationRandom);
	Huecheck = HueVariationRandom + range;
	//Debug.Log("Huecheck " + Huecheck);
	CheckHue();
	//Debug.Log("Huecheck " + Huecheck);
	mycolor.r = Huecheck/255;//+addColor;
	
	HueVariationRandom = Random.Range(-HueVariationDiff , HueVariationDiff) ;
	Huecheck = HueVariationRandom + range;
	CheckHue();
	mycolor.g = Huecheck/255;//+addColor;
	
	HueVariationRandom = Random.Range(-HueVariationDiff , HueVariationDiff) ;
	Huecheck = HueVariationRandom + range;
	CheckHue();
	mycolor.b = Huecheck/255;//+addColor;
	
//	Debug.Log(grey);
	gameObject.renderer.material.SetColor("_SpecColor", mycolor);
}

function CheckHue(){	
	if(Huecheck>255)
	Huecheck=255;	
	if(Huecheck<0)
	Huecheck=0;
	if(Huecheck>maxDiff)
	Huecheck=maxDiff;
	if(Huecheck<minDiff)
	Huecheck=minDiff;
}