$(document).ready(function(){
		  $(".cor").toggle(function(){
		    $(this).css("color","red")
		    ;},
		    function(){
		    $(this).css("color","#646464");
		    }
		  );
		  
		  $(".bottom_i").toggle(function(){
		  	$(this).css("color","red")
		    $(".bottom_sp").css("color","red").html("已收藏");
		    ;},
		    function(){
		    $(this).css("color","#646464");
		    $(".bottom_sp").css("color","#646464").html("收藏");
		    }
		  );
		  
		$(".dibu_a").toggle(function(){
		    $(this).css("color","red");},
		    function(){
		    $(this).css("color","#ffffff");}
		  );
		  

		//////TAB切换
		function bor(){
			var uu = document.getElementById("content");
			var as = uu.getElementsByTagName("a");
			for(var i=0;i<as.length;i++){
				as[i].onclick=function(){
					for(var i=0;i<as.length;i++){
						as[i].className="";
					}
						this.className="bor";
				}
			}
		}
		bor();
});



	



