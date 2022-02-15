<template>
<div>
  <Navbar></Navbar>
  <ProductionReport></ProductionReport>
</div>
</template>

<script>
import Navbar from "@/components/DashboardComponents/Navbar";
import ProductionReport from "@/components/DashboardComponents/ProductionReport";
export default {
  name: "ProductionReport_Dashboard",
  components: {
    Navbar,
    ProductionReport,
  }
}
</script>

<style scoped>

</style>