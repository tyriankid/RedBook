function Dsy()
{
this.Items = {};
}
Dsy.prototype.add = function(id,iArray)
{
this.Items[id] = iArray;
};
Dsy.prototype.Exists = function(id)
{
if(typeof(this.Items[id]) == "undefined") return false;
return true;
};

function change(v){
var str="0";
    for (i = 0; i < v; i++) {
        str += ("_" + (document.getElementById(s[i]).selectedIndex - 1));
    }
    var ss=document.getElementById(s[v]);
with(ss){
  length = 0;
  options[0]=new Option(opt0[v],opt0[v]);
  if(v && document.getElementById(s[v-1]).selectedIndex>0 || !v)
  {
   if(dsy.Exists(str)){
    ar = dsy.Items[str];
    for(i=0;i<ar.length;i++)options[length]=new Option(ar[i],ar[i]);
    if(v)options[1].selected = true;
   }
  }
  if(++v<s.length){change(v);}
}
}
function change2(v){
var str="0";
    for (i = 0; i < v; i++) {
        str += ("_" + (document.getElementById(s2[i]).selectedIndex - 1));
    }
    var ss=document.getElementById(s2[v]);
with(ss){
  length = 0;
  options[0]=new Option(opt0[v],opt0[v]);
  if(v && document.getElementById(s2[v-1]).selectedIndex>0 || !v)
  {
   if(dsy.Exists(str)){
    ar = dsy.Items[str];
    for(i=0;i<ar.length;i++)options[length]=new Option(ar[i],ar[i]);
    if(v)options[1].selected = true;
   }
  }
  if(++v<s2.length){change2(v);}
}
}
function change3(v){
var str="0";
    for (i = 0; i < v; i++) {
        str += ("_" + (document.getElementById(s3[i]).selectedIndex - 1));
    }
    var ss=document.getElementById(s3[v]);
with(ss){
  length = 0;
  options[0]=new Option(opt0[v],opt0[v]);
  if(v && document.getElementById(s3[v-1]).selectedIndex>0 || !v)
  {
   if(dsy.Exists(str)){
    ar = dsy.Items[str];
    for(i=0;i<ar.length;i++)options[length]=new Option(ar[i],ar[i]);
    if(v)options[1].selected = true;
   }
  }
  if(++v<s3.length){change3(v);}
}
}
var dsy = new Dsy();

dsy.add("0", ["英雄联盟"]);
dsy.add("0_0_0", ["艾欧尼亚", "暗影岛", "班德尔城", "裁决之地", "钢铁烈阳", "黑色玫瑰", "教育网专区", "巨神峰", "均衡教派", "卡拉曼达", "雷瑟守备", "诺克萨斯", "皮城警备", "皮尔特沃夫", "守望之海", "水晶之痕", "影流", "战争学院", "征服之海", "祖安"]);
dsy.add("0_0", ["电信", "网通", "体验服"]);
dsy.add("0_0_1", ["比尔吉沃特", "德玛西亚", "弗雷尔卓德", "无畏先锋", "恕瑞玛", "扭曲丛林", "巨龙之巢"]);
dsy.add("0_0_2", ["试练之地"]);



// var s=["province","city","county"];
//var opt0 = ["","",""];
var opt0 = ["游戏名称","服务器","游戏大区"];
function setup()
{
for(i=0;i<s.length-1;i++)
  document.getElementById(s[i]).onchange=new Function("change("+(i+1)+")");
change(0);
}
function setup2()
{
for(i=0;i<s2.length-1;i++)
  document.getElementById(s2[i]).onchange=new Function("change2("+(i+1)+")");
change2(0);
}
function setup3()
{
for(i=0;i<s3.length-1;i++)
  document.getElementById(s3[i]).onchange=new Function("change3("+(i+1)+")");
change3(0);
}