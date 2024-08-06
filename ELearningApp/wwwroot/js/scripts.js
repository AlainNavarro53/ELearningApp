document.addEventListener('DOMContentLoaded', () => {
    fetch('/api/courses')
        .then(response => response.json())
        .then(courses => {
            const courseList = document.getElementById('course-list');
            courses.forEach(course => {
                const li = document.createElement('li');
                li.textContent = course.title;
                courseList.appendChild(li);
            });
        })
        .catch(error => console.error('Error:', error));
});