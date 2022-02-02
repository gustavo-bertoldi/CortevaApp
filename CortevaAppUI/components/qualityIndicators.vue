<template>
    <div align="center">
        <div align="center" class="col productionName rcorners2">
            Qualité
        </div>

        <div align="center" class="col rcorners2">
            Affichage des indicateurs de qualité
        </div>
    </div>

</template>

<script>
    export default {
        name: "qualityIndicators"
    }
</script>

<style scoped>

    .productionName {
        left: 0;
        top: 0;
        min-width: 150px;
        max-width: 250px;

        margin-bottom: 50px;
    }

    .rcorners1 {
        border-radius: 25px;
        background: lightblue;
        padding: 20px;
        margin-bottom: 30px;
        width: 180px;

    }


    .rcorners2 {
        border-radius: 25px;
        border: 2px solid lightblue;
        padding: 10px;
    }
</style>
