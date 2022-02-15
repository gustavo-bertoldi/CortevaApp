<template>
  <div>
    <Navbar></Navbar>
    <PackaginglineID></PackaginglineID>
  </div>
</template>

<script>
import Navbar from "@/components/DashboardComponents/Navbar";
import PackaginglineID from "@/components/DashboardComponents/PackaginglineID";

export default {
  name: "PackagingLineID_Dashboard",
  components: {
    Navbar,
    PackaginglineID,
  }
}
</script>

<style scoped>

</style>