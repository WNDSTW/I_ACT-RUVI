$(function () {
  $('input').iCheck({
    checkboxClass: 'icheckbox_square-blue',
    radioClass: 'iradio_square-blue',
    increaseArea: '20%' // optional
  });
  jQuery("form").submit(function(){
    var notf = jQuery("div.alert");
    notf.removeClass("alert-warning");
    notf.removeClass("alert-error");
    notf.html("");
    notf.addClass("alert-warning alert-dismissable");
    notf.append("<i class='icon fa fa-exclamation'></i>");
    notf.html(notf.html()+"Proses pengecekan username dan password");
    jQuery.post(
      jQuery(this).attr("action"),
      jQuery(this).find("input:eq(0)").attr("name")+"="+
      jQuery(this).find("input:eq(0)").val()+"&"+
      jQuery(this).find("input:eq(1)").attr("name")+"="+
      jQuery(this).find("input:eq(1)").val(),
      function(data)
      {
        setTimeout(function(){
          notf.removeClass("alert-info");
          notf.html("");
          if(data.length>0){
            notf.addClass("alert-success");
            notf.append("<i class='icon fa fa-thumbs-up'></i>");
            notf.html(notf.html()+"Anda berhasil login");
          } else {
            notf.addClass("alert-error");
            notf.append("<i class='icon fa fa-warning'></i>");
            notf.html(notf.html()+"Maaf username/password anda salah!");
          }
        },2000);
        console.log(data);
        return false;
      }
    );
    return false;
  });
});