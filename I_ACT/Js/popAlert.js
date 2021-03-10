
function successMessage(){
    swal({
        type: 'success',
        title: 'Information',
        html: "<div id='boxBerhasil' class='alert alert-success alert-dismissible' style='text-align:center !important;display: block;font-size:20px' role='alert'> Data Is Saved Succesfully. </div>",
        width: '800px',
    });
    $('#modalAdd').modal('hide');
}

function updatedMessage(){
    swal({
        type: 'success',
        title: 'Information',
        html: "<div id='boxUpdated' class='alert alert-success alert-dismissible' style='text-align:center !important;display: block;font-size:20px' role='alert'> Data Is Updated Succesfully. </div>",
        width: '800px',
    });
    $('#modalAdd').modal('hide');
}


function errorMessage(exception){
    swal({
        type: 'error',
        title: 'Error, Data Gagal Disimpan !',
        html: "<div id='boxError' class='alert alert-danger alert-dismissible' style='text-align:center !important;display: block;font-size:20px' role='alert'> '" + exception + "' </div>",
        width: '800px',
        footer: 'Segera Hubungi IT Programmer (ext. 5132) untuk memperbaiki Error',
    });
}



function confirmDelete(ctl, event) {
    var defaultAction = $(ctl).prop("href");
    // CANCEL DEFAULT LINK BEHAVIOUR
    event.preventDefault();
    swal({
        title: 'Are You Sure ?',
        html: "<div id='boxKonfirmasi' class='alert alert-info alert-dismissible' style='text-align:center !important;display: block;font-size:20px' role='alert'> You won't be able to revert this! </div>",
        width: '800px',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Delete This Data!'
    }).then((result) => {
        if (result.value) {
                swal(
                  'Data Has Been deleted!',
                  '',
                  'success'
                );
              window.location.href = defaultAction;
                 return true;
            }else{
                swal(
                  'Cancelled',
                  '',
                  'error'
                );
                return false;
            }   
        })
}

