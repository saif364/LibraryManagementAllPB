
//#region Sub Course Section 
var courseIndex = $('#coursesTable tbody tr').length - 1;//for first row skip to model binding
$('#addCourse').click(function () {
    var courseName = $('#courseName').val();
    var teacherName = $('#teacherName').val();
    var maximumStudents = $('#maximumStudents').val();

    if (courseName && teacherName && maximumStudents) {
        $('#coursesTable tbody').append(`
            <tr id="row_${courseIndex}">
                <td>${courseName}</td>
                <td>${teacherName}</td>
                <td>${maximumStudents}</td>
                <td>
                    <button type="button" class="btn btn-sm btn-danger" onclick="removeCourse(${courseIndex})">Remove</button>
                </td>
                <input type="hidden" name="StudentSubCourses[${courseIndex}].CourseName" value="${courseName}" />
                <input type="hidden" name="StudentSubCourses[${courseIndex}].TeacherName" value="${teacherName}" />
                <input type="hidden" name="StudentSubCourses[${courseIndex}].MaximumStudent" value="${maximumStudents}" />
            </tr>
        `);

        var firstRow = $("#coursesTable tbody tr:first");

        firstRow.find("input").each(function () {
            $(this).val("");
        })
        courseIndex++;
    } else {
        alert("Please fill in all course details.");
    }
});

function removeCourse(index) {
    $('#row_' + index).remove();
    reIndexCourses();
}

function reIndexCourses() {
    debugger;
    var rows = $('#coursesTable tbody tr');
    courseIndex = -1; //for first row skip to model binding

    rows.each(function () {
        $(this).attr('id', 'row_' + courseIndex);
        $(this).find('button').attr('onclick', 'removeCourse(' + courseIndex + ')');
        $(this).find('input[name^="StudentSubCourses"]').each(function () {
            var fieldName = $(this).attr('name');

            if (fieldName.includes('CourseName')) {
                $(this).attr('name', 'StudentSubCourses[' + courseIndex + '].CourseName');
            } else if (fieldName.includes('TeacherName')) {
                $(this).attr('name', 'StudentSubCourses[' + courseIndex + '].TeacherName');
            } else if (fieldName.includes('MaximumStudent')) {
                $(this).attr('name', 'StudentSubCourses[' + courseIndex + '].MaximumStudent');
            }
        });

        courseIndex++;
    });
}

//#endregion


//#region Sub Attachemnt Section 
var courseIndex = 0;

$('#addFile').click(function () {
    var fileInput = $('#idFile')[0];
    var file = fileInput.files[0]; // Get the selected file

    if (file) {
        // Clone the original file input (without the value) and append it for form purposes
        var newFileInput = $(fileInput).clone().attr('id', 'fileInput_' + courseIndex).hide();

        // Append the file details to the table and include the new (cloned) file input
        $('#idfilesTable tbody').append(`
            <tr id="row_${courseIndex}">
                <td id="fileNameTd_${courseIndex}">
                    ${file.name}  
                </td>
                <td>
                    <button type="button" class="btn btn-sm btn-danger" onclick="removeFile(${courseIndex})">Remove</button>
                </td>
            </tr>
        `);

        // Append the cloned input (which now becomes part of the form data) to the hidden part of the DOM, within the same row
        $('#fileNameTd_' + courseIndex).append(newFileInput);

        // Clear the original file input after cloning and adding the file
        $(fileInput).val('');

        courseIndex++;
    } else {
        toastr.warning("Please select a file before adding.");
    }
});

function removeFile(index) {
    $('#row_' + index).remove();
    $('#file_' + index).remove();
    $('input[name="StudentSubAttachmentsFiles[' + index + ']"]').remove();
    //reIndexFiles();
}

//function reIndexFiles() {
//    var rows = $('#idfilesTable tbody tr');
//    fileIndex = 0;
//    rows.each(function () {
//        $(this).attr('id', 'row_' + fileIndex);
//        fileIndex++;
//    });
//}

//#endregion


$('#btnStudentAdd').click(function (e) {
    e.preventDefault();
    debugger;

    var formData = new FormData();

    formData.append('Id', $('#Id').val());
    formData.append('Name', $('#Name').val());
    formData.append('Address', $('#Address').val());
    formData.append('Mobile', $('#Mobile').val());

    // child 
    var StudentSubCourses = [];
    $('#coursesTable tbody tr').each(function () {
        var courseName = $(this).find('input[name*="CourseName"]').val();
        var teacherName = $(this).find('input[name*="TeacherName"]').val();
        var maximumStudent = $(this).find('input[name*="MaximumStudent"]').val();

        if (courseName && teacherName && maximumStudent) {
            var courseData = {
                CourseName: courseName,
                TeacherName: teacherName,
                MaximumStudent: maximumStudent
            };
            StudentSubCourses.push(courseData);
             
        }
    });
    StudentSubCourses.forEach(function (courseData, index) {
        formData.append(`StudentSubCourses[${index}].CourseName`, courseData.CourseName);
        formData.append(`StudentSubCourses[${index}].TeacherName`, courseData.TeacherName);
        formData.append(`StudentSubCourses[${index}].MaximumStudent`, courseData.MaximumStudent);
    });
    //
    
    $('#idfilesTable tbody tr').each(function () {
        debugger;
        var fileInputElement = $(this).find('input[name*="StudentSubAttachmentsFiles"]')[0];

        if (fileInputElement && fileInputElement.files.length > 0) {
            // Now safely access the first file
            var fileInput = fileInputElement.files[0];
            formData.append('StudentSubAttachmentsFiles', fileInput);
        }
    });

    $.ajax({
        url: '/Student/Update',
        type: 'POST',
        processData: false, // Important for sending FormData
        contentType: false,  // Important for sending FormData
        data: formData,
        success: function (response) {
            if (response.success) {
                var redirectUrl = response.redirectUrl + "?message=" + encodeURIComponent(response.message);
                window.location.href = redirectUrl;
            } else {
                toastr.warning(response.message);
            }
        },
        error: function (xhr, status, error) {
            toastr.warning('Error: ' + xhr.responseText);
        }
    });
});









