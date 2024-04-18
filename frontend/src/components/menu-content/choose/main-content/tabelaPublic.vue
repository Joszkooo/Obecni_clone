<template>
  <div id="tabela">
    <table>
      <thead class="rounded-header">
      <tr>
        <th>Pseudonim</th>
        <th>Czas Pracy</th>
        <th>Wejście</th>
        <th>Wyjście</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="item in items" :key="item.id" :class="{ 'odd-row': index % 2 === 0, 'even-row': index % 2 === 1 }">
        <td>{{ item.id }}</td>
        <td>{{ item.Imie }}</td>
        <td>{{ item.Nazwisko }}</td>
        <td>{{ item.Pseudonim }}</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
//import tableComponents from 'table.js'
</script>

<style scoped>
#tabela {
  margin-top: 50px;
  margin-right: 2.5%;
  margin-left: 2.5%;
}

table {
  width: 100%;
  border-collapse: collapse;
  color: white;
  font-size: 1.4vw;
  font-weight: lighter;
  font-family: "Open sans";
  margin-top: 15%;
}

th, td {
  padding: 20px;
}
td {
  text-align: center;
}
th {
  font-weight: lighter;
}
thead{
  background-color:  #101936;
  text-align: center;
}
tbody {
  border-radius: 20%;
}
.rounded-header th:first-child {
  border-top-left-radius: 10px; /* Zaokrąglamy lewy górny róg */
  border-bottom-left-radius: 10px;
}
.tbody-margin {
  margin-top: 10px; /* Dodaj margines między thead a tbody */
}
.rounded-header th:last-child {
  border-top-right-radius: 10px; /* Zaokrąglamy prawy górny róg */
  border-bottom-right-radius: 10px; /* Zaokrąglamy prawy górny róg */
}
.odd-row {
  background-color: #212B4E; /* Kolor tła dla wierszy o nieparzystych indeksach */
}

.even-row {
  background-color: #101936; /* Kolor tła dla wierszy o parzystych indeksach */
}
</style>
