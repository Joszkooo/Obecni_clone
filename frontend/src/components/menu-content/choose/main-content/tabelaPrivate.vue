<template>
  <div id="tabela-container">
    <table class="tabelanoscroll">
      <thead class="rounded-header">
      <tr>
        <th>Pracownik</th>
        <th>Status</th>
        <th>Czas Pracy</th>
        <th>Wejście</th>
        <th>Wyjście</th>
        <th>Urlop [h]</th>
        <th>Tygodniówka</th>
      </tr>
      </thead>
    </table>
    <div class="tabelascroll">
      <table>
        <tbody>
        <tr v-for="(item, index) in items" :key="item.id" :class="{ 'odd-row': index % 2 === 0, 'even-row': index % 2 === 1 }">
          <td>{{ item.id }}</td>
          <td>{{ item.status }}</td>
          <td>{{ item.CzasPracy }}</td>
          <td>{{ item.Wejscie }}</td>
          <td>{{ item.Wyjscie }}</td>
          <td>{{ item.Urlop }}</td>
          <td>{{ item.tygodniowka }}</td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>

  export default {
    data: function () {
      return {
        items: [
          {
            id: "Julka - Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },
          {
            id: "Julka - Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },
          {
            id: "Julka - Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },
          {
            id: "Julka - Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },
          {
            id: "Julka - Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },
          {
            id: "Julka - Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },
          {
            id: "Julka - Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },
          {
            id: "Julka - Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },
          {
            id: "Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },        {
            id: "Julka - Julia Grzywińska",
            status: 'Produkt 1',
            CzasPracy: 10,
            Wejscie: '8:50',
            Wyjscie: '13:30',
            Urlop: 10,
            tygodniowka: "01d01h59min"
          },
        ]
      };
    }
  };
</script>


<style scoped>
.tabelanoscroll {
  margin-bottom: 15px;
}

.tabelascroll {
  overflow-x: auto;
  max-height: 300px; /* Ustaw maksymalną wysokość dla obszaru przewijania */
  border-radius: 10px;
}

#tabela-container {
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
}

th, td {
  padding: 15px;
  text-align: center;
}

th {
  font-weight: lighter;
}

thead {
  background-color: #101936;
  text-align: center;
}

tbody {
  border-radius: 20%;
}

.rounded-header th:first-child {
  border-top-left-radius: 10px;
  border-bottom-left-radius: 10px;
}

.rounded-header th:last-child {
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
}

.odd-row {
  background-color: #212B4E;
}

.even-row {
  background-color: #101936;
}

thead th {
  position: sticky;
  top: 0;
  z-index: 1;
}
.tabelascroll::-webkit-scrollbar {
  width: 12px; /* Szerokość suwaka */
}

.tabelascroll::-webkit-scrollbar-track {
  background: #101936; /* Kolor tła ścieżki suwaka */
}

.tabelascroll::-webkit-scrollbar-thumb {
  background-color: #6B72FF; /* Kolor suwaka */
}
.tabelascroll::-webkit-scrollbar-button:vertical {
  height: 20px; /* Wysokość przycisku suwaka */
}

.tabelascroll::-webkit-scrollbar-button:horizontal {
  width: 20px; /* Szerokość przycisku suwaka */
}

.tabelascroll::-webkit-scrollbar-button::before {
  content: "▲"; /* Dodaj strzałkę */
  color: white; /* Kolor strzałki */
  font-size: 12px; /* Rozmiar strzałki */
}

</style>
