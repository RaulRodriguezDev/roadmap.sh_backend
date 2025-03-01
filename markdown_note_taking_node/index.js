import express from 'express'

const app = new express()

app.listen(3000, () => {
	console.log('App listenning on port 3000')
})
