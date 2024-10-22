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

        firstRow.each(function myfunction() {
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
