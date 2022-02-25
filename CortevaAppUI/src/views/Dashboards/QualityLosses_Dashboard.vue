<template>
<div>
  <Navbar></Navbar>
  <QualityLosses></QualityLosses>
</div>
</template>

<script>
import Navbar from "@/components/DashboardComponents/Navbar";
import QualityLosses from "@/components/DashboardComponents/QualityLosses";

export default {
  name: "QualityLosses_Dashboard",
  components: {
    Navbar,
    QualityLosses,
  }
}
</script>

<style scoped>

</style>