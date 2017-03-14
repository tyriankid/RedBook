<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modifymenujson.aspx.cs" Inherits="RedBookPlatform.admin.modifymenujson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑权限</title>
    <script src="../Resources/js/zTree/jquery-1.4.4.min.js"></script>
    <link href="../Resources/js/zTree/zTreeStyle.css" rel="stylesheet" />
    <script src="../Resources/js/zTree/jquery.ztree.core-3.5.js"></script>
    <script src="../Resources/js/zTree/jquery.ztree.excheck-3.5.js"></script>
    <link href="../Resources/js/zTree/bootstrap.min.css" rel="stylesheet" />
    <style>

        .ztree>li {float:left}
          .ztree {width:100%}

        .ztree>li{ line-height:17px; padding:0px 5px; border-right:1px solid #666;}


    </style>
  
    <script type="text/javascript">
		
     

        var setting = {
            showLine: false,
            check: {
                enable: true,
                chkboxType : {  "Y" : "p", "N" : "s"  },
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            view: {
                // addDiyDom: addDiyDom

            
                    showLine: false
              
            },
            callback: {
        onCheck: zTreeOnCheck
            },

           
        };

    var zNodes;
    $.ajax({
        type: "POST",
        contentType: "application/json", //WebService 会返回Json类型
        url: "modifymenujson.aspx/GetZnodes", //调用WebService的地址和方法名称组合 ---- WsURL/方法名
        async: false,
        success: function (result) {     //回调函数，result，返回值
            var json = JSON.parse('[' + result.d + ']');
            zNodes = json;
        }
    });
        
    $(document).ready(function () {
        $.fn.zTree.init($("#treeDemo"), setting, zNodes);

        var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
        var nodes = treeObj.getNodesByFilter(filter);

   
        var newNodes = [{ name: "查看", value: "1" }, { name: "维护", value: "2" }, { name: "全部", value: "3" }];
        for (var i = 0; i < nodes.length; i++) {
        
            treeObj.addNodes(nodes[i], newNodes, true);
        }
      
        treeObj.expandAll(true);

     
        

    });

    function filter(node) {
        return (node.id.length == 6 );
    }
 
    function zTreeOnCheck(event, treeId, treeNode)
    {
        var treeObj = $.fn.zTree.getZTreeObj("treeDemo");


      if (treeNode.value == 1 || treeNode.value == 2||treeNode.value == 3) {
            var children = treeNode.getParentNode().children
            for (var i = 0; i < children.length; i++) {
                if (children[i].value != treeNode.value) {
                    children[i].checked = false;
                    treeObj.updateNode(children[i]);
                }

            }
      }

      if ( treeNode.id!=undefined&&treeNode.id.length == 6 && treeNode.checked == true) {
          var children = treeNode.children
          for (var i = 0; i < children.length; i++) {
              if (children[i].value == 3) {
                  children[i].checked = true;
                  treeObj.updateNode(children[i]);
              }

          }
         
      }

      if (treeNode.id != undefined&&treeNode.id.length == 4 && treeNode.checked == true) {
          var nodelev1 = treeNode.children
         
          for (var i = 0; i < nodelev1.length; i++) {
             
              nodelev1[i].checked = true;
              treeObj.updateNode(nodelev1[i]);

              for (var k = 0; k < nodelev1[i].children.length; k++) {
                 
                  if (nodelev1[i].children[k].value == 3) {
                     
                      nodelev1[i].children[k].checked = true;
                      treeObj.updateNode(nodelev1[i].children[k]);
                  }
              }

          }

      }
      if (treeNode.id != undefined&&treeNode.id.length == 2 && treeNode.checked == true) {
          var nodelev1 = treeNode.children
      
          for (var i = 0; i < nodelev1.length; i++) {
             
              nodelev1[i].checked = true;
              treeObj.updateNode(nodelev1[i]);
                
              for (var k = 0; k < nodelev1[i].children.length; k++) {
                  nodelev1[i].children[k].checked = true;
                  treeObj.updateNode(nodelev1[i].children[k]);
                      for (var x = 0; x < nodelev1[i].children[k].children.length; x++) {
                          if (nodelev1[i].children[k].children[x].value == 3) {
                              nodelev1[i].children[k].children[x].checked = true;
                              treeObj.updateNode(nodelev1[i].children[k].children[x]);
                              }

                          }

               
                  }

          }

      }
    
    
      
    }

    function save()
    {   
            var string = "";          
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
            var nodes = zTree.getCheckedNodes(true);
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].id == undefined) {
                    continue;
                }
                if (nodes[i].id.length == 6)
                {
                    
                    var children = nodes[i].children;
                    var v = 1;
                    for (var k = 0;k < children.length;k++) {
                        if (children[k].checked == true)
                        {
                            v = children[k].value;
                        }
                    }

                    string += "{\"name\":\"" +  nodes[i].name + "\",";
                    string += "\"href\": \"" + nodes[i].href + "\",";
                    string += "\"id\": \"" + nodes[i].id + "\",";
                    string += "\"code\": " + v + "}";
                }

                else
                {
                    string += "{\"name\":\"" + nodes[i].name + "\",";
                    string += "\"href\": \"\",";
                    string += "\"id\": \"" + nodes[i].id + "\",";
                    string += "\"code\":\"\" }";
                }

            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "modifymenujson.aspx/toSave",
                data: "{json:'" + string + "'}",
                dataType: 'json',
                success: function (result) {
                    alert(result.d);
                   
                }
            });

    }

    //页面载入时,同时载入选中项
    function loadSelect() {
        $.ajax({
            type: "POST",
            contentType: "application/json", //WebService 会返回Json类型
            url: "modifymenujson.aspx/GetSelect", //调用WebService的地址和方法名称组合 ---- WsURL/方法名
            async: false, //同步调用(如果是异步,则会慢一拍)
            success: function (result) {     //回调函数，result，返回值
                if (result.d != "") {
                    var nodes = JSON.parse(result.d)
                    if (nodes != null) {
                        for (var i = 0; i < nodes.length; i++) {
                            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
                            var node = zTree.getNodeByParam("id", nodes[i].id);
                            if (node != null) {
                                zTree.checkNode(node, true, false);
                            }
                            if (node.id.length == 6) {
                                for (var k = 0; k < node.children.length; k++) {
                                    if (nodes[i].code == node.children[k].value) {
                                        node.children[k].checked = true;
                                        zTree.updateNode(node.children[k]);
                                    }
                                }
                            }
                        }
                    }

                }
            }
        });

       
    }
        </script>
</head>
<body  onload="loadSelect()">
    <form id="form1" runat="server">
        <div style="margin-top:10px">
              <div style="float:left;margin-left:10px" > <input  type="button" style="margin-left:450px"   class="btn btn-primary"  value="保存" onclick="save()"/></div>  
            <div style="float:left;width:150%"><ul id="treeDemo" class="ztree" style="width:100%; overflow:auto;"></ul></div>  

        </div>
    </form>
</body>
</html>
