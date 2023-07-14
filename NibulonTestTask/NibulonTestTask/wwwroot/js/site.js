let table1Btn = document.getElementById('table1-btn');
let table2Btn = document.getElementById('table2-btn');
let table3Btn = document.getElementById('table3-btn');

let startDate;
let finishDate;

function getLastStartEndDates() {


    let selectStartDate = document.getElementById("start-date");
    let selectFinishDate = document.getElementById("finish-date");

    console.log(selectStartDate.selectedIndex)
    console.log(selectFinishDate.selectedIndex)

    let eIndex1 = selectStartDate.selectedIndex
    let eIndex2 = selectFinishDate.selectedIndex

    startDate = selectStartDate.options[eIndex1].text;
    finishDate = selectFinishDate.options[eIndex2].text;
}




table1Btn.addEventListener('click', (e) => {
    $('#tab-container').load(`/Home/Table1`)
})

table2Btn.addEventListener('click', (e) => {

    getLastStartEndDates();
    $('#tab-container').load(`/Home/Table2?startDate=${startDate}&endDate=${finishDate}`)
})

table3Btn.addEventListener('click', (e) => {
    getLastStartEndDates();

    $('#tab-container').load(`/Home/Table3?startDate=${startDate}&endDate=${finishDate}`)
})

let groupBtn = document.getElementById('group-btn');
groupBtn.addEventListener('click', (e) => {

    getLastStartEndDates();
    $('#tab-container').load(`/Home/Table2?startDate=${startDate}&endDate=${finishDate}`)

})

