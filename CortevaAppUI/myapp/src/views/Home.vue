<template>
  <div class="home">
    <navbar-saisie></navbar-saisie>
    <img alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>

    {{ $t('labeler')}}
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import NavbarSaisie from "@/components/NavbarSaisie";

export default {
  name: 'Home',
  components: {
    NavbarSaisie,
    HelloWorld
  }
}
</script>
