var courseIndex = $('#coursesTable tbody tr').length-1;

$('#addCourse').click(function () {
    var courseName = $('#courseName').val();
    var teacherName = $('#teacherName').val();
    var maximumStudents = $('#maximumStudents').val();

    if (courseName && teacherName && maximumStudents) {
        // Add row to the table
        $('#coursesTable tbody').append(`
                                                    <tr id="row_${courseIndex}">
                                                        <td>${courseName}</td>
                                                        <td>${teacherName}</td> 
                                                        <td>${maximumStudents}</td>
                                                        <td><button type="button" class="btn btn-sm btn-danger" onclick="removeCourse(${courseIndex})">Remove</button></td>
                                                    </tr>
                                                `);
            //hidden inputs add for model binding
        $('#hiddenCourses').append(`
            <input type="hidden" name="StudentSubCourses[${courseIndex}].CourseName" value="${courseName}" />
            <input type="hidden" name="StudentSubCourses[${courseIndex}].TeacherName" value="${teacherName}" />
            <input type="hidden" name="StudentSubCourses[${courseIndex}].MaximumStudent" value="${maximumStudents}" />
        `);
        
        $('#courseName').val('');
        $('#teacherName').val('');
        $('#maximumStudents').val('');

        courseIndex++;
    } else {
        alert("Please fill in all course details.");
    }
});

 
function removeCourse(index) {
    $('#row_' + index).remove();
    $('input[name="StudentSubCourses[' + index + '].CourseName"]').remove();
    $('input[name="StudentSubCourses[' + index + '].TeacherName"]').remove();
    $('input[name="StudentSubCourses[' + index + '].MaximumStudent"]').remove();

    //reIndexCourses();
}

//function reIndexCourses() {
//    debugger;
//    var rows = $('#coursesTable tbody tr');
//    courseIndex = 0;

//    // Re-index the rows in the table
//    rows.each(function () {
//        $(this).attr('id', 'row_' + courseIndex); // Update the row id

//        // Update the remove button's onclick function to use the new index
//        $(this).find('button').attr('onclick', 'removeCourse(' + courseIndex + ')');

//        courseIndex++;
//    });

//    // Re-index the hidden inputs in the hiddenCourses div
//    courseIndex = 0; // Reset courseIndex to re-index hidden inputs
//    $('#hiddenCourses input[name^="StudentSubCourses"]').each(function () {
//        var fieldName = $(this).attr('name');

//        if (fieldName.includes('CourseName')) {
//            $(this).attr('name', 'StudentSubCourses[' + courseIndex + '].CourseName');
//        } else if (fieldName.includes('TeacherName')) {
//            $(this).attr('name', 'StudentSubCourses[' + courseIndex + '].TeacherName');
//        } else if (fieldName.includes('MaximumStudent')) {
//            $(this).attr('name', 'StudentSubCourses[' + courseIndex + '].MaximumStudent');
//        }

//        courseIndex++;
//    });
//}
