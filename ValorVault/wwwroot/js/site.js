const userProfilesContainer = document.querySelector('.user-profiles');

// Запит до бази даних для отримання профілів користувачів
fetch('https://your-api-endpoint/get-user-profiles')
    .then(response => response.json())
    .then(data => {
        // Перебір масиву даних з профілями користувачів
        data.forEach(profile => {
            // Створення HTML-коду для картки користувача
            const userProfileHTML = `
                <div class="user-profile">
                    <h2>${profile.name}</h2>
                    <p>Email: ${profile.email}</p>
                    <p>Створено анкет: ${profile.createdSurveys}</p>
                    <p>Опубліковано анкет: ${profile.publishedSurveys}</p>
                    <p>Відхилено анкет: <span class="math-inline">\{profile\.rejectedSurveys\}</p\>
<button data\-user\-id\="</span>{profile.id}">Видалити</button>
                </div>
            `;