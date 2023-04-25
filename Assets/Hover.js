var TextObject:TextMesh;
var rend:Renderer;
var Desc1:String;
var Desc2:String;
var Desc3:String;
var Desc4:String;
var Desc5:String;
var HighLightColor:Color;
var DefaultColor:Color;
var HasDesc:boolean;

function OnMouseOver(){
    rend = GetComponent.<Renderer>();
    rend.material.color = HighLightColor;
    if(HasDesc){
        TextObject.GetComponent(TextMesh).text=Desc1 +"\n"+Desc2+"\n"+Desc3+"\n"+Desc4+"\n"+Desc5;
    }
}

function OnMouseExit(){
    rend = GetComponent.<Renderer>();
    rend.material.color = DefaultColor;
    if(HasDesc){
        TextObject.GetComponent(TextMesh).text="";
    }
}