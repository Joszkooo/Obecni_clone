<template>
  <div id="tabela">
    <h2>Tabela</h2>
    <table>
      <thead>
      <tr>
        <th>ID</th>
        <th>Nazwa</th>
        <th>Cena</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="item in items" :key="item.id">
        <td>{{ item.id }}</td>
        <td>{{ item.name }}</td>
        <td>{{ item.price }}</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  data() {
    return {
      items: [
        { id: 1, name: 'Produkt 1', price: 10 },
        { id: 2, name: 'Produkt 2', price: 20 },
        { id: 3, name: 'Produkt 3', price: 30 },
        // Dodaj więcej danych, jeśli potrzebujesz
      ]
    };
  }
};
</script>

<style scoped>
#tabela {
  margin-top: 20px;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  border: 1px solid #ddd;
  padding: 8px;
}

th {
  background-color: #f2f2f2;
}
</style>
