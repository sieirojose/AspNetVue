<template>
  <div class="container">
    <h1>Cat Facts</h1>

    <!-- Input para el filtro -->
    <div class="filter">
      <input type="text"
             v-model="filterText"
             placeholder="Filter cat facts..."
             class="filter-input" />
    </div>

    <!-- Tabla de resultados -->
    <table class="table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Fact</th>
          <th>Searched On</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="fact in filteredCatFacts" :key="fact.id">
          <td>{{ fact.id }}</td>
          <td>{{ fact.fact }}</td>
          <td>{{ formatDate(fact.searchedOn) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
  import { defineComponent, onMounted, ref, computed } from 'vue';
  import { getCatFacts } from '@/services/CatFactsService';

  export default defineComponent({
    name: 'CatFactsGrid',
    setup() {
      const catFacts = ref<{ id: number; fact: string; searchedOn: string }[]>([]);
      const filterText = ref(''); // Texto de filtro

      // Fetch de datos
      const fetchCatFacts = async () => {
        const data = await getCatFacts();
        catFacts.value = data;
      };

      const formatDate = (date: string) => {
        return new Date(date).toLocaleString();
      };

      // Computed para filtrar catFacts en tiempo real
      const filteredCatFacts = computed(() => {
        return catFacts.value.filter((fact) =>
          fact.fact.toLowerCase().includes(filterText.value.toLowerCase())
        );
      });

      onMounted(fetchCatFacts);

      return { catFacts, filterText, filteredCatFacts, formatDate };
    },
  });
</script>

<style scoped>
  .container {
    width: 100%;
    margin: 20px auto;
    padding: 10px;
  }

  h1 {
    text-align: center;
    margin-bottom: 20px;
    color: #333;
    font-size: 2.5rem;
  }

  .filter {
    text-align: center;
    margin-bottom: 20px;
  }

  .filter-input {
    width: 50%;
    padding: 10px;
    font-size: 1rem;
    border: 1px solid #ccc;
    border-radius: 5px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }

  .table {
    width: 100%;
    border-collapse: collapse;
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
    background-color: #fff;
    border-radius: 8px;
    overflow: hidden;
  }

  th, td {
    padding: 15px;
    text-align: left;
  }

  th {
    background-color: #4CAF50;
    color: white;
    font-size: 1.2rem;
    text-transform: uppercase;
  }

  td {
    border-bottom: 1px solid #ddd;
    font-size: 1rem;
  }

  tr:nth-child(even) {
    background-color: #f9f9f9;
  }

  tr:hover {
    background-color: #f1f1f1;
  }

  @media (max-width: 768px) {
    .filter-input {
      width: 90%;
    }

    .table {
      font-size: 0.9rem;
    }

    th, td {
      padding: 10px;
    }
  }
</style>
