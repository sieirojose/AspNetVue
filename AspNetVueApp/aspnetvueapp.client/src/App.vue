<template>

    <!-- Contenedor principal que permite usar todo el ancho -->
    <div class="full-width">
      <CatFactsGrid />
    </div>

</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import CatFactsGrid from '@/components/CatFactsGrid.vue';

  export default defineComponent({
    name: 'App',
    components: { CatFactsGrid },
  });
</script>

<style>

  #app {
    margin: 0;
    padding: 0;
    width: 100%;
    display: flex;
    justify-content: center; 
  }

  .full-width {
    width: 100%; 
    max-width: 100%;
  }
</style>
