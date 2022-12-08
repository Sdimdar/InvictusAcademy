<template>
  <q-page>
    <div id="jitsiContainer" style="height: 100%; width: 100%;"></div>
  </q-page>
</template>

<script>
export default {
  name: 'RoomComponent',
  data () {
    return {
      name: '',
      address: ''
    }
  },
  mounted () {
    this.name = this.$route.params.name
    this.address = this.$route.params.address
    this.loadScript('https://meet.jit.si/external_api.js', () => {
      if (!window.JitsiMeetExternalAPI) throw new Error('Jitsi Meet API not loaded')
      this.embedJitsiWidget()
    })
  },
  beforeMount () {
    this.removeJitsiWidget()
  },
  methods: {
    loadScript (src, cb) {
      const scriptEl = document.createElement('script')
      scriptEl.src = src
      scriptEl.async = 1
      document.querySelector('head').appendChild(scriptEl)
      scriptEl.addEventListener('load', cb)
    },
    embedJitsiWidget () {
      const domain = 'meet.jit.si'
      const options = {
        roomName: this.address,
        width: 1080,
        height: 700,
        parentNode: document.querySelector('#jitsiContainer')
      }
      this.jitsiApi = new window.JitsiMeetExternalAPI(domain, options)
    },
    executeCommand (command, ...value) {
      this.jitsiApi.executeCommand(command, ...value)
    },
    addEventListener (event, fn) {
      this.jitsiApi.on(event, fn)
    },
    removeJitsiWidget () {
      if (this.jitsiApi) this.jitsiApi.dispose()
    }
  }
}
</script>

<style scoped>

</style>
