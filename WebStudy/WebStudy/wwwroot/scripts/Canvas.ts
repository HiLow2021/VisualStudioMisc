/// <reference path="../typings/three/three.d.ts" />

class Canvas {
    renderer: THREE.WebGLRenderer;

    constructor(canvas: HTMLCanvasElement) {
        this.renderer = new THREE.WebGLRenderer({
            canvas: canvas
        });
        this.renderer.setPixelRatio(window.devicePixelRatio);
        this.renderer.setSize(canvas.width, canvas.height);
    }

    execute() {
        const scene = new THREE.Scene();
        const camera = new THREE.PerspectiveCamera(45, this.renderer.domElement.width / this.renderer.domElement.height);
        camera.position.set(0, 0, 1000);

        const geometry = new THREE.TorusGeometry(300, 100, 64, 100);
        const material = new THREE.MeshStandardMaterial({ color: 0x6699FF, roughness: 0 });
        const mesh = new THREE.Mesh(geometry, material);
        scene.add(mesh);

        const light = new THREE.DirectionalLight(0xFFFFFF, 1);
        light.position.set(100, 100, 100);
        scene.add(light);

        const renderer = this.renderer;

        tick();

        // 毎フレーム時に実行されるループイベントです。
        function tick() {
            mesh.rotation.x += 0.01;
            mesh.rotation.y += 0.01;

            renderer.render(scene, camera);

            // 次のフレームの要求をします。
            requestAnimationFrame(tick);
        }
    }
}